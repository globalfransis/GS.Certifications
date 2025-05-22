using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static GS.Certifications.Application.UseCases.Socios.Certificaciones.Services.CertificacionService;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class RechazarDocumentoSolicitudCertificacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Observaciones { get; set; }
    public byte[] RowVersion { get; set; }
}

public class RechazarDocumentoSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, RechazarDocumentoSolicitudCertificacionCommand, Unit>
{
    private readonly ICertificationsDbContext Context;
    private readonly ICertificacionService certificacionService;

    public RechazarDocumentoSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        Context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<Unit> HandleRequestAsync(RechazarDocumentoSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var documentoSolicitudUpdate = new SolicitudCertificacionDocumentoUpdate()
            {
                EstadoId = DocumentoEstado.RECHAZADO,
                Observaciones = request.Observaciones,
                RowVersion = request.RowVersion,
            };

            await certificacionService.UpdateDocumentoAsync(request.Id, documentoSolicitudUpdate);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
