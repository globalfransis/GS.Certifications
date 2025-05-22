using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static GS.Certifications.Application.UseCases.Socios.Certificaciones.Services.CertificacionService;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class RechazarSolicitudCertificacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Observaciones { get; set; }
    public byte[] RowVersion { get; set; }
    //public string ArchivoURL { get; set; }
    //public int? Version { get; set; }
    //public DateTime? FechaDesde { get; set; }
    //public DateTime? FechaHasta { get; set; }
    //public DateTime? FechaSubida { get; set; }
}

public class ValidarSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, RechazarSolicitudCertificacionCommand, Unit> // Adjust TEntity and TResponse properly
{
    private readonly ICertificationsDbContext Context;
    private readonly ICertificacionService certificacionService;

    public ValidarSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        Context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<Unit> HandleRequestAsync(RechazarSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var fechaSolicitud = DateTime.Now;

            var solicitudUpdateParameters = new SolicitudCertificacionUpdate()
            {
                EstadoId = SolicitudCertificacionEstado.RECHAZADA,
                Observaciones = request.Observaciones,
                //FechaSolicitud = fechaSolicitud,
                UltimaModificacionEstado = fechaSolicitud,
                PropietarioId = Origen.SOCIOS
            };

            await certificacionService.UpdateSolicitudAsync(request.Id, solicitudUpdateParameters);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
