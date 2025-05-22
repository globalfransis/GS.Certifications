using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static GS.Certifications.Application.UseCases.Socios.Certificaciones.Services.CertificacionService;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class AprobarSolicitudCertificacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Observaciones { get; set; }
    public byte[] RowVersion { get; set; }
    //public string ArchivoURL { get; set; }
    //public int? Version { get; set; }
    public DateTime? VigenciaDesde { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    //public DateTime? FechaSubida { get; set; }
}

public class AprobarSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, AprobarSolicitudCertificacionCommand, Unit> // Adjust TEntity and TResponse properly
{
    private readonly ICertificationsDbContext Context;
    private readonly ICertificacionService certificacionService;

    public AprobarSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        Context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<Unit> HandleRequestAsync(AprobarSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var fechaSolicitud = DateTime.Now;

            var solicitudUpdateParameters = new SolicitudCertificacionUpdate()
            {
                EstadoId = SolicitudCertificacionEstado.APROBADA,
                VigenciaDesde = request.VigenciaDesde,
                VigenciaHasta = request.VigenciaHasta,
                Observaciones = request.Observaciones,
                UltimaModificacionEstado = fechaSolicitud,
                PropietarioId = Origen.BACKOFFICE
            };

            await certificacionService.UpdateSolicitudAsync(request.Id, solicitudUpdateParameters);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (AprobacionSolicitudDocumentosInvalidosException ex)
        {
            throw new ValidationErrorException("Documentos", ex.Message);
        }
        catch (SolicitudVigenciaNulaException ex)
        {
            throw new ValidationErrorException("Vigencia", ex.Message);
        }
        catch (SolicitudVigenciaInvalidaException ex)
        {
            throw new ValidationErrorException("Vigencia", ex.Message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
