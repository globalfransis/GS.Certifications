using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class UpdateDocumentoSolicitudCertificacionDraftCommand : IRequest<Unit>, ISolicitudCertificacionDocumentoUpdate
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
    public string ArchivoURL { get; set; }
    public string Observaciones { get; set; }
    public int? Version { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public DateTime? FechaSubida { get; set; }
}

public class UpdateDocumentoSolicitudCertificacionDraftCommandHandler : BaseRequestHandler<Unit, UpdateDocumentoSolicitudCertificacionDraftCommand, Unit> // Adjust TEntity and TResponse properly
{
    private readonly ICertificationsDbContext Context;
    private readonly ICertificacionService certificacionService;

    public UpdateDocumentoSolicitudCertificacionDraftCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        Context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<Unit> HandleRequestAsync(UpdateDocumentoSolicitudCertificacionDraftCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await certificacionService.UpdateDocumentoDraftAsync(request.Id, request);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
