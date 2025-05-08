using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class VerifyComprobanteCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
    public string NroIdentificacionFiscalPro { get; set; }
    public string DomicilioPro { get; set; }
    public short? CategoriaTipoEmisorId { get; set; }
    public short? ComprobanteTipoId { get; set; }
    public int? PuntoVenta { get; set; }
    public string Letra { get; set; }
    public int? Numero { get; set; }
    public DateTime? FechaEmision { get; set; }
    public short? TipoCodigoAutorizacionId { get; set; }
    public string CodigoAutorizacion { get; set; }
    public long? MonedaId { get; set; }
    public decimal? ImporteNeto { get; set; }
    public decimal? ImporteTotal { get; set; }
    public int? EmpresaId { get; set; }
    public long? CompanyId { get; set; }
    public bool? ValidacionQR { get; set; } = false;
    public string QRValor { get; set; }
    public decimal? ImporteIVA { get; set; }
    public decimal? ImporteBonificacion { get; set; } = decimal.Zero;
    public decimal? ImportePercepcionIVA { get; set; } = decimal.Zero;
    public decimal? ImportePercepcionIIBB { get; set; } = decimal.Zero;
    public decimal? ImportePercepcionMunicipal { get; set; } = decimal.Zero;
    public decimal? ImporteImpuestosInternos { get; set; } = decimal.Zero;
    public decimal? ImporteOtrosTributosProv { get; set; } = decimal.Zero;
    public string CodigoHES { get; set; }
    public int? ComprobanteEstadoId { get; set; }
    public string Comentarios { get; set; }
    public List<ComprobanteDetalleCreate> Detalles { get; set; }
    public List<ComprobanteImpuestoCreate> Impuestos { get; set; }
    public List<ComprobantePercepcionCreate> Percepciones { get; set; }
}

public class VerifyComprobanteCommandHandler : BaseRequestHandler<Unit, VerifyComprobanteCommand, Unit>
{
    private readonly ICertificationsDbContext Context;
    private readonly IComprobanteService comprobanteService;

    public VerifyComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
    {
        Context = context;
        this.comprobanteService = comprobanteService;
    }

    protected override async Task<Unit> HandleRequestAsync(VerifyComprobanteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await comprobanteService.VerifyEstadoARCAAsync(request.Id, request.RowVersion);

            await Context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
        // TODO: catch exception if AFIP verification fails
        catch (Exception)
        {
            throw;
        }
    }
}