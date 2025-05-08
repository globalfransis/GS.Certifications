using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class UpdateComprobanteDraftCommand : IRequest<Unit>
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
    public List<ComprobanteDetalleUpdate> Detalles { get; set; }
    public List<ComprobanteImpuestoUpdate> Impuestos { get; set; }
    public List<ComprobantePercepcionUpdate> Percepciones { get; set; }

    public bool? ValidacionQR { get; set; } = false;
    public string QRValor { get; set; }

    public long CompanyId { get; set; }

    public decimal? Cotizacion { get; set; }
    public string NroRemito { get; set; }
    public string NroOrdenCompra { get; set; }
    public short? CondicionVentaId { get; set; }

    public DateTime? FechaVencimiento { get; set; }
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
}

public class UpdateComprobanteDraftCommandHandler : BaseRequestHandler<Unit, UpdateComprobanteDraftCommand, Unit> // Adjust TEntity and TResponse properly
{
    private readonly ICertificationsDbContext Context;
    private readonly IComprobanteService comprobanteService;

    public UpdateComprobanteDraftCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
    {
        Context = context;
        this.comprobanteService = comprobanteService;
    }

    protected override async Task<Unit> HandleRequestAsync(UpdateComprobanteDraftCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var companyExtra = await Context.CompanyExtras.FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId) ?? throw new Exception("No existen datos extra de la Company.");

            var comprobanteSaveArgs = new ComprobanteUpdate()
            {
                Id = request.Id,
                //NroIdentificacionFiscalPro = request.NroIdentificacionFiscalPro,
                //DomicilioPro = request.DomicilioPro,
                CategoriaTipoEmisorId = (short)request.CategoriaTipoEmisorId,
                ComprobanteTipoId = (short)request.ComprobanteTipoId,
                PuntoVenta = (int)request.PuntoVenta,
                Numero = (int)request.Numero,
                FechaEmision = (DateTime)request.FechaEmision,
                TipoCodigoAutorizacionId = (short)request.TipoCodigoAutorizacionId,
                CodigoAutorizacion = request.CodigoAutorizacion,
                MonedaId = (long)request.MonedaId,
                ImporteNeto = request.ImporteNeto ?? 0,
                ImporteTotal = request.ImporteTotal ?? 0,
                ImporteBonificacion = request.ImporteBonificacion ?? 0,
                Comentarios = request.Comentarios,
                Detalles = new(request.Detalles),
                Impuestos = new(request.Impuestos),
                Percepciones = new(request.Percepciones),

                ImporteIVA = request.ImporteIVA ?? 0,
                ImporteImpuestosInternos = (decimal)request.ImporteImpuestosInternos,
                ImporteOtrosTributosProv = (decimal)request.ImporteOtrosTributosProv,
                ImportePercepcionIIBB = (decimal)request.ImportePercepcionIIBB,
                ImportePercepcionIVA = (decimal)request.ImportePercepcionIVA,
                ImportePercepcionMunicipal = (decimal)request.ImportePercepcionMunicipal,
                ValidacionQR = request.ValidacionQR,
                QRValor = request.QRValor,


                FechaVencimiento = request.FechaVencimiento,
                FechaVencimientoCodigoAutorizacion = request.FechaVencimientoCodigoAutorizacion,

                Cotizacion = request.Cotizacion,
                NroRemito = request.NroRemito,
                NroOrdenCompra = request.NroOrdenCompra,
                CondicionVentaId = request.CondicionVentaId,

                // Company
                CategoriaTipoReceptorId = companyExtra.TipoResponsableId,
                //NroIdentificacionFiscalCompany = companyExtra.IdentificadorTributario,
                CompanyId = request.CompanyId
            };

            await comprobanteService.UpdateDraftAsync(comprobanteSaveArgs);
            await Context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
