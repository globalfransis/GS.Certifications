using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Commands;

public class CreateSolicitudCertificacionCommand : IRequest<int>
{
    public int? SocioId { get; set; } = 0;
    public int? CertificacionId { get; set; }
    public short? EstadoId { get; set; }
    public short? CantidadAprobaciones { get; set; } = 0;
    public string Observaciones { get; set; }
}

public class SolicitudCreationParams : ISolicitudCertificacionCreate
{
    public int SocioId { get; set; } = 0;
    public int CertificacionId { get; set; }
    public short? EstadoId { get; set; }
    public short CantidadAprobaciones { get; set; } = 0;
    public string Observaciones { get; set; }
}

public class CreateSolicitudCertificacionCommandHandler : BaseRequestHandler<int, CreateSolicitudCertificacionCommand, int>
{
    private readonly ICertificationsDbContext context;
    private readonly ICertificacionService certificacionService;

    public CreateSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        this.context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<int> HandleRequestAsync(CreateSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var creationParams = new SolicitudCreationParams()
            {
                SocioId = (int)request.SocioId,
                CertificacionId = (int)request.CertificacionId,
                EstadoId = request.EstadoId ?? SolicitudCertificacionEstado.BORRADOR,
                CantidadAprobaciones = request.CantidadAprobaciones ?? 0,
                Observaciones = request.Observaciones
            };

            var nuevo = await certificacionService.CreateSolicitudAsync(creationParams);
            context.SolicitudCertificaciones.Add(nuevo);
            
            await context.SaveChangesAsync(cancellationToken);
            
            return nuevo.Id;
        }
        catch (CertificacionInexistenteException ex)
        {
            throw new ValidationErrorException("CertificacionId", ex.Message);
        }
        catch (YaExisteSolicitudCertificacionException ex)
        {
            throw new ValidationErrorException("CertificacionId", ex.Message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}