using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class UpdateDocumentoSolicitudCertificacionCommand : IRequest<Unit>, ISolicitudCertificacionDocumentoUpdate
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
    public string ArchivoURL { get; set; }
    public string Observaciones { get; set; }
    public int? Version { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public DateTime? FechaSubida { get; set; }
    public short? EstadoId { get; set; }
    public long? ValidadoPorId { get; set; }
    public string MotivoRechazo { get; set; }
}

public class UpdateDocumentoSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, UpdateDocumentoSolicitudCertificacionCommand, Unit> // Adjust TEntity and TResponse properly
{
    private readonly ICertificationsDbContext Context;
    private readonly ICertificacionService certificacionService;

    public UpdateDocumentoSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        Context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<Unit> HandleRequestAsync(UpdateDocumentoSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await certificacionService.UpdateDocumentoAsync(request.Id, request);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (DocumentoVigenciaNulaException ex)
        {
            throw new ValidationErrorException("Vigencia", ex.Message);
        }
        catch (DocumentoVigenciaInvalidaException ex)
        {
            throw new ValidationErrorException("Vigencia", ex.Message);
        }
        catch (DocumentoArchivoNuloException ex)
        {
            throw new ValidationErrorException("DocumentoError", ex.Message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
