using GS.Certifications.Application.CQRS.DbContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

// --- Estrategia de Cabecera ---
public class OCRHeaderStrategy : IComprobanteHeaderStrategy
{
    private readonly ICertificationsDbContext _context;

    public OCRHeaderStrategy(ICertificationsDbContext context)
    {
        _context = context;
    }

    public async Task PopulateHeaderAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando OCRHeaderStrategy...");
        var comprobanteResult = context.ResultInProgress;

        Log.Logger.Information("Procesando cabecera desde campos extraídos.");
        await PopulateHeaderFromFieldsAsync(context);
        comprobanteResult.ValidacionQR = false;


        // NroRemito, NroOrdenCompra, CondicionVenta...
        await PopulateOtherFieldsAsync(context);


        Log.Logger.Debug("OCRHeaderStrategy finalizada.");
    }

    private async Task PopulateOtherFieldsAsync(AnalisysContext context)
    {
        var comprobante = context.ResultInProgress;
        var extractedFields = context.ExtractedFields;

        comprobante.RazonSocialPro = extractedFields.GetValueOrDefault("RazonSocialEmisor")?.ValueString.Trim();
        comprobante.NroRemito = extractedFields.GetValueOrDefault("NroRemito")?.ValueString;
        comprobante.NroOrdenCompra = extractedFields.GetValueOrDefault("NroOrdenCompra")?.ValueString;

        var condicionVentaExtraida = extractedFields.GetValueOrDefault("CondicionVenta")?.ValueString;

        var condicionVentaBuscada = (await _context.CondicionVentas.FirstOrDefaultAsync(ct => ct.Descripcion.Contains(condicionVentaExtraida) || condicionVentaExtraida.Contains(ct.Descripcion)))?.Idm ?? CondicionVenta.OTRO;

        comprobante.CondicionVentaId = condicionVentaBuscada;
        comprobante.FechaVencimiento = extractedFields.GetValueOrDefault("FechaVencimiento")?.ValueDate?.DateTime;
        comprobante.FechaVencimientoCodigoAutorizacion = extractedFields.GetValueOrDefault("FechaVencimientoCodigoAutorizacion")?.ValueDate?.DateTime;

    }

    private async Task PopulateHeaderFromFieldsAsync(AnalisysContext context)
    {
        var comprobante = context.ResultInProgress;
        var extractedFields = context.ExtractedFields;

        string rawCuitReceptor = extractedFields.GetValueOrDefault("IdentificadorReceptor")?.ValueString ?? string.Empty;
        string rawCuitEmisor = extractedFields.GetValueOrDefault("IdentificadorEmisor")?.ValueString ?? string.Empty;

        var cuitReceptorSoloDigitosEnum = rawCuitReceptor.Where(char.IsDigit);
        var cuitEmisorSoloDigitosEnum = rawCuitEmisor.Where(char.IsDigit);

        comprobante.NroIdentificacionFiscalCompany = new string(cuitReceptorSoloDigitosEnum.ToArray());
        comprobante.NroIdentificacionFiscalPro = new string(cuitEmisorSoloDigitosEnum.ToArray());

        var comprobanteTipo = extractedFields.GetValueOrDefault("TipoComprobante")?.ValueString?.TrimStart('0'); // quitamos ceros a izq
        var comprobanteTipoBuscado = await _context.ComprobanteTipos.FirstOrDefaultAsync(ct => ct.CodigoArca.Equals(comprobanteTipo));
        comprobante.ComprobanteTipoId = comprobanteTipoBuscado?.Idm;
        comprobante.Numero = extractedFields.GetValueOrDefault("Nro")?.ValueString;
        comprobante.PuntoVenta = extractedFields.GetValueOrDefault("PuntoVenta")?.ValueString;
        comprobante.FechaEmision = extractedFields.GetValueOrDefault("FechaEmision")?.ValueDate?.ToString("yyyy-MM-dd")?.Split("T").FirstOrDefault();
        var tipoCodigoAutorizacionExtraido = extractedFields.GetValueOrDefault("TipoCodigoAutorizacion")?.ValueString;
        var codigoAutorizacionExtraido = extractedFields.GetValueOrDefault("CodigoAutorizacion")?.ValueString;
        string monedaExtraida = extractedFields.GetValueOrDefault("Moneda")?.ValueString;
        comprobante.MonedaId = (await _context.Currencies.FirstOrDefaultAsync(ct => ct.Symbol.Contains(monedaExtraida) || monedaExtraida.Contains(ct.Symbol)))?.Idm;

        if (comprobante.MonedaId is null && context.Parameters.EmpresaId is not null)
        {
            var empresaPortal = await _context.EmpresasPortales.Include(e => e.Monedas).FirstOrDefaultAsync();
            var monedaDefault = empresaPortal?.Monedas.FirstOrDefault(m => m.MonedaPorDefecto)?.CurrencyId;
            comprobante.MonedaId = monedaDefault;
        }

        var tipoCodigoAutorizacion = await _context.CodigoAutorizacionTipos.FirstOrDefaultAsync(ct => ct.Descripcion.ToLower() == tipoCodigoAutorizacionExtraido.ToLower());

        comprobante.TipoCodigoAutorizacionId = tipoCodigoAutorizacion != null ? tipoCodigoAutorizacion.Idm : CodigoAutorizacionTipo.CAE;
        comprobante.CodigoAutorizacion = codigoAutorizacionExtraido;
    }

    // Necesitaríamos un helper inverso para TipoCodAut letra -> ID en ComprobanteAnalysisHelper
}
