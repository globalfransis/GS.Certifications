using GSF.Domain.Entities.Companies;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.MailProcessors;
using System;
using System.Collections.Generic;

namespace GS.Certifications.Application.Interfaces.EmailProcessor;

public record EmailInvoiceAttachmentDto
{
    public Company Company { get; set; }
    public List<EmpresaPortal> EmpresasPortalesAsignadas { get; set; }
    public ComprobanteEMailExtension ComprobanteEMailExtension { get; set; }
    public string EmailId { get; set; }
    public string EmailSubject { get; set; }
    public string EmailTo { get; set; }
    public string EmailFrom { get; set; }
    public DateTimeOffset? EmailReceivedDateTime { get; set; }
    public string FileName { get; set; }
    public string FileId { get; set; }
    public string FileContentType { get; set; }
    public int? FileSize { get; set; }
    public byte[] FileContentBytes { get; set; }
}
