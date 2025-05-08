using GSF.Domain.Entities.Companies;
using GSF.Microsoft.Graph.Mail.Commons.Dtos;
using GSF.Microsoft.Graph.Mail.Commons.Enums;
using GSF.Microsoft.Graph.Mail.Services.Components.Reader;
using GSF.Microsoft.Graph.Mail.Services.Components.Reader.Request;
using GSF.Microsoft.Graph.Mail.Services.Components.Reader.Request.Filters;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.Interfaces.EmailProcessor;
using GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;
using GS.Certifications.Domain.Entities.MailProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Services.EmailProcessor;

public class EmailInvoiceService : IEmailInvoiceService
{

    private readonly ICertificationsDbContext _dbContext;

    public EmailInvoiceService(ICertificationsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<EmailInvoiceAttachmentDto>> GetEmailInvoiceAttachmentsAsync(long companyId)
    {
        IntegracionFacturaPorCorreo integration = await GetIntegrationAsync(companyId);
        IEnumerable<MailMessage> messages = await GetMailMessagesAsync(integration);
        return ProcessMailMessagesAsync(integration, messages);
    }

    private async Task<IntegracionFacturaPorCorreo> GetIntegrationAsync(long companyId)
    {
        Company company = await _dbContext.Companies.Where(c => c.Id == companyId).SingleOrDefaultAsync();
        IntegracionFacturaPorCorreo integration = await _dbContext.IntegracionesFacturaPorCorreo.Include(i => i.Detalles).ThenInclude(d => d.EmpresaPortal).Where(i => i.Company == company).SingleOrDefaultAsync();
        if (integration.Actvive is not true) throw new CompanyNotAllowedForIntegracionFacturaPorCorreoException(company);
        return integration;
    }
    private async Task<List<MailMessage>> GetMailMessagesAsync(IntegracionFacturaPorCorreo integration)
    {
        string clientId = integration.ClientId;
        string tenantId = integration.TenantId;
        string clientSecret = integration.ClientSecret;
        string MailsTo = integration.MailsTo;
        string subjectKey = integration.SubjectKey;
        string mailFolder = string.IsNullOrWhiteSpace(integration.MailFolder) ? "Inbox" : integration.MailFolder;
        DateTimeOffset? ultimoRecibido = await _dbContext.ComprobanteEMailExtensiones
                                                                           .Where(c => c.CompanyId == integration.CompanyId)
                                                                           .OrderByDescending(c => c.EmailReceivedDateTime)
                                                                           .Select(c => c.EmailReceivedDateTime)
                                                                           .FirstOrDefaultAsync();

        MailReaderComponent mailreader = new(new MailReaderComponentConfiguration(clientId, tenantId, clientSecret, MailsTo, 10, 2));

        MailReaderFilterBuilder filterBuilder = new();
        filterBuilder.Begin()
            .HasAttachments(true);

        if (!string.IsNullOrWhiteSpace(subjectKey))
        {
            filterBuilder.Begin().AndSubject(subjectKey, StringComparisonType.Cointains);
        }

        if (ultimoRecibido is not null)
        {
            var fecha = (DateTime)ultimoRecibido?.UtcDateTime.AddMilliseconds(1000);
            filterBuilder.Begin().RecivedDateTime(fecha, DateComparisonType.AfterOrEquals);
        }

        MailReaderRequest request = new(mailFolder, null, true, filterBuilder);
        IEnumerable<MailMessage> messages = await mailreader.GetMessagesAsync(request);

        return messages.ToList();
    }

    private List<EmailInvoiceAttachmentDto> ProcessMailMessagesAsync(IntegracionFacturaPorCorreo integration, IEnumerable<MailMessage> messages)
    {
        List<EmailInvoiceAttachmentDto> comprobantes = new();

        foreach (MailMessage message in messages)
        {
            List<IntegracionFacturaPorCorreoDetalle> integracionesDetalles = integration.Detalles
                .Where(d =>
                    d.Actvive &&
                    (string.IsNullOrWhiteSpace(d.SubjectKey) || message.Subject.Contains(d.SubjectKey, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(d.MailsFrom) || d.MailsFrom
                        .Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Any(mailPattern =>
                            {
                                string pattern = mailPattern.Trim();
                                if (pattern.StartsWith("*@"))
                                {
                                    // Validación por dominio
                                    string domain = pattern.Substring(1); // Quita "*"
                                    return message.From.EndsWith(domain, StringComparison.OrdinalIgnoreCase);
                                }
                                else
                                {
                                    // Validación exacta
                                    return pattern.Equals(message.From, StringComparison.OrdinalIgnoreCase);
                                }
                            }))).ToList();

            foreach (MailMessageAttachment attachment in message.Attachments)
            {
                if (attachment is MailMessageAttachment fileAttachment &&
                    fileAttachment.Name.EndsWith(".PDF", StringComparison.OrdinalIgnoreCase) &&
                    fileAttachment.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                {
                    ComprobanteEMailExtension comprobanteEMailExtension = new()
                    {
                        EmailId = message.Id,
                        EmailSubject = message.Subject,
                        EmailFrom = message.From,
                        EmailReceivedDateTime = message.ReceivedDateTime,
                        FileName = attachment.Name,
                        FileId = attachment.Id,
                    };

                    _dbContext.ComprobanteEMailExtensiones.Add(comprobanteEMailExtension);

                    comprobantes.Add(new EmailInvoiceAttachmentDto()
                    {
                        Company = integration.Company,
                        EmpresasPortalesAsignadas = integracionesDetalles.Select(i => i.EmpresaPortal).ToList(), // Verificar si puede ser nulo
                        ComprobanteEMailExtension = comprobanteEMailExtension,

                        EmailId = message.Id,
                        EmailSubject = message.Subject,
                        EmailFrom = message.From,
                        EmailReceivedDateTime = message.ReceivedDateTime,
                        FileName = attachment.Name,
                        FileId = attachment.Id,
                        FileContentType = attachment.ContentType,
                        FileSize = attachment.Size,
                        FileContentBytes = attachment.ContentBytes
                    });
                }
            }
        }

        return comprobantes;
    }

}
