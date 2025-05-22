using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static GS.Certifications.Application.UseCases.Socios.Certificaciones.Services.CertificacionService;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class ValidarDocumentoSolicitudCertificacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    //public short? EstadoId { get; set; }
    public long? ValidadoPorId { get; set; }
    public string Observaciones { get; set; }
    public byte[] RowVersion { get; set; }
}

public class ValidarDocumentoSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, ValidarDocumentoSolicitudCertificacionCommand, Unit>
{
    private readonly ICertificationsDbContext Context;
    private readonly ICertificacionService certificacionService;

    public ValidarDocumentoSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
    {
        Context = context;
        this.certificacionService = certificacionService;
    }

    protected override async Task<Unit> HandleRequestAsync(ValidarDocumentoSolicitudCertificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var documentoSolicitudUpdate = new SolicitudCertificacionDocumentoUpdate()
            {
                EstadoId = DocumentoEstado.VALIDADO,
                Observaciones = request.Observaciones,
                FechaDesde = request.FechaDesde,
                FechaHasta = request.FechaHasta,
                ValidadoPorId = request.ValidadoPorId,
                RowVersion = request.RowVersion
            };

            await certificacionService.UpdateDocumentoAsync(request.Id, documentoSolicitudUpdate);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (DocumentoVigenciaInvalidaException ex)
        {
            throw new ValidationErrorException("Vigencia", ex.Message);
        }
        catch (DocumentoVigenciaNulaException ex)
        {
            throw new ValidationErrorException("Vigencia", ex.Message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
