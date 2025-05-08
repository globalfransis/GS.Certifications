using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.Interfaces.SfeApi;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using GS.Certifications.Application.Commons.Dtos.SfeApi;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;
using GS.Certifications.Domain.Entities.Comprobantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

// --- Estrategia de Cabecera ---
public class QRHeaderStrategy : IComprobanteHeaderStrategy
{
    private readonly ICertificationsDbContext _context;
    private readonly IValidarComprobanteService _validarComprobanteService;

    public QRHeaderStrategy(ICertificationsDbContext context/*, IValidarComprobanteService validarComprobanteService*/)
    {
        _context = context;
        //_validarComprobanteService = validarComprobanteService;
    }

    public async Task PopulateHeaderAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando QRHeaderStrategy...");
        var comprobanteResult = context.ResultInProgress;
        bool qrProcessedSuccessfully = false;

        var qrBarcode = context.AnalysisResult.Barcodes?.FirstOrDefault(b => b.Kind == AI.DocumentIntelligence.Legacy.Models.DocumentBarcodeKind.QRCode);
        if (qrBarcode != null && !string.IsNullOrEmpty(qrBarcode.Value))
        {
            string url = qrBarcode.Value;
            string base64Code = ComprobanteAnalysisHelper.GetBase64CodeFromUrl(url);
            if (!string.IsNullOrEmpty(base64Code))
            {
                context.QrJson = ComprobanteAnalysisHelper.DecodeBase64(base64Code);
                if (!string.IsNullOrEmpty(context.QrJson))
                {
                    qrProcessedSuccessfully = await TryPopulateHeaderFromQrAsync(context);
                }
            }
        }

        // 2. Procesamos campos extraídos si QR falló o no existía
        if (!qrProcessedSuccessfully && context.ExtractedFields != null)
        {
            Log.Logger.Information("QR no procesado o inválido.");
            comprobanteResult.ValidacionQR = false;
        }
        else if (!qrProcessedSuccessfully)
        {
            Log.Logger.Warning("No se pudo procesar cabecera: sin campos extraídos.");
        }

        // NroRemito, NroOrdenCompra, CondicionVenta...
        await PopulateOtherFieldsAsync(context);


        Log.Logger.Debug("QRHeaderStrategy finalizada.");
    }

    private async Task PopulateOtherFieldsAsync(AnalisysContext context)
    {
        var comprobante = context.ResultInProgress;
        var extractedFields = context.ExtractedFields;

        comprobante.NroRemito = extractedFields.GetValueOrDefault("NroRemito")?.ValueString;
        comprobante.NroOrdenCompra = extractedFields.GetValueOrDefault("NroOrdenCompra")?.ValueString;

        var condicionVentaExtraida = extractedFields.GetValueOrDefault("CondicionVenta")?.ValueString;

        var condicionVentaBuscada = (await _context.CondicionVentas.FirstOrDefaultAsync(ct => ct.Descripcion.Contains(condicionVentaExtraida) || condicionVentaExtraida.Contains(ct.Descripcion)))?.Idm ?? CondicionVenta.OTRO;

        comprobante.CondicionVentaId = condicionVentaBuscada;
        comprobante.FechaVencimiento = extractedFields.GetValueOrDefault("FechaVencimiento")?.ValueDate?.DateTime;
        comprobante.FechaVencimientoCodigoAutorizacion = extractedFields.GetValueOrDefault("FechaVencimientoCodigoAutorizacion")?.ValueDate?.DateTime;

    }

    private async Task<bool> TryPopulateHeaderFromQrAsync(AnalisysContext context)
    {
        IComprobanteCabecera cabecera;
        try
        {
            cabecera = JsonConvert.DeserializeObject<ComprobanteCabecera>(context.QrJson);
            if (cabecera == null) throw new ComprobanteQRInvalido("JSON del QR es nulo o inválido.");
        }
        catch (JsonException jsonEx)
        {
            throw new ComprobanteQRInvalido($"Error deserializando JSON del QR: {jsonEx.Message}", jsonEx);
        }

        var comprobanteResult = context.ResultInProgress;
        comprobanteResult.QRValor = context.QrJson;

        // --- Validar QR contra ARCA ---
        try
        {
            var companyExtra = await _context.CompanyExtras.AsNoTracking().FirstOrDefaultAsync(c => c.CompanyId == context.Parameters.CompanyId)
                ?? throw new InvalidOperationException($"No se encontraron datos de CompanyExtra para CompanyId {context.Parameters.CompanyId}");

            if (!long.TryParse(companyExtra.IdentificadorTributario, out long cuitCompany))
            {
                throw new InvalidOperationException($"El Identificador Tributario de CompanyExtra no es un CUIT válido para CompanyId {context.Parameters.CompanyId}");
            }

            var request = new ValidarComprobanteRequestDto()
            {
                Modo = cabecera.tipoCodAut switch
                {
                    "E" => nameof(CodigoAutorizacionTipo.CAE),
                    "I" => nameof(CodigoAutorizacionTipo.CAI),
                    "A" => nameof(CodigoAutorizacionTipo.CAEA),
                    _ => nameof(CodigoAutorizacionTipo.CAE),
                },
                CuitEmisor = cabecera.cuit,
                PtoVta = cabecera.ptoVta,
                CbteTipo = cabecera.tipoCmp,
                CbteNro = cabecera.nroCmp,
                CbteFch = DateOnly.Parse(cabecera.fecha),
                ImpTotal = decimal.ToDouble(cabecera.importe),
                CodAutorizacion = cabecera.codAut.ToString(),
                // check
                DocNroReceptor = cabecera.nroDocRec,
                DocTipoReceptor = 80
            };

            //var response = await _validarComprobanteService.ValidarComprobanteAsync(cuitCompany, request);
            var response = await _validarComprobanteService.ValidarComprobanteAsync(30707466967, request); // TODO: quitar el cuit de globalsis hardcodeado cuando se defina de donde va a salir el cuit operador

            comprobanteResult.EstadoConstatacionQR = response.Autorizado ? EstadoConstatacion.Ok : EstadoConstatacion.Rechazado; // O Rechazado? Usar Error es más genérico
            if (response.Observaciones != null && response.Observaciones.Any())
            {
                comprobanteResult.ObservacionesARCAQR = "Obs QR: " + string.Join("; ", response.Observaciones);
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Fallo técnico al validar QR contra ARCA (CUIT Emisor {Cuit})", cabecera.cuit);
            comprobanteResult.EstadoConstatacionQR = EstadoConstatacion.Error;
            comprobanteResult.ObservacionesARCAQR = $"Error técnico validando QR: {ex.Message}";
            // ¿Debería esto impedir usar los datos del QR? Probablemente sí.
            // throw; // Relanzar para que el llamador sepa que la validación falló técnicamente.
            // O retornar false para que se usen los datos del documento. Decidimos retornar false.
            return false;
        }

        comprobanteResult.FechaEmision = cabecera.fecha;
        comprobanteResult.NroIdentificacionFiscalCompany = cabecera.nroDocRec.ToString();
        comprobanteResult.NroIdentificacionFiscalPro = cabecera.cuit.ToString();
        comprobanteResult.PuntoVenta = cabecera.ptoVta.ToString();
        var cmpTipoBuscado = await _context.ComprobanteTipos.AsNoTracking().FirstOrDefaultAsync(ct => ct.CodigoArca == cabecera.tipoCmp.ToString());
        comprobanteResult.ComprobanteTipoId = cmpTipoBuscado?.Idm;
        comprobanteResult.Letra = cmpTipoBuscado?.Letra;

        comprobanteResult.Numero = cabecera.nroCmp.ToString();
        comprobanteResult.ImporteTotal = cabecera.importe;

        string monedaCode = cabecera.moneda == "PES" ? "ARS" : cabecera.moneda;
        comprobanteResult.MonedaId = (await _context.Currencies.AsNoTracking().FirstOrDefaultAsync(c => c.Code == monedaCode))?.Idm;

        if (comprobanteResult.MonedaId is null && context.Parameters.EmpresaId is not null)
        {
            var empresaPortal = await _context.EmpresasPortales.Include(e => e.Monedas).FirstOrDefaultAsync();
            var monedaDefault = empresaPortal?.Monedas.FirstOrDefault(m => m.MonedaPorDefecto)?.CurrencyId;
            comprobanteResult.MonedaId = monedaDefault ?? 1; // Pesos por defecto
        }

        //comprobanteResult.NroIdentificacionFiscalCompany = cabecera.nroDocRec.ToString();
        //comprobanteResult.CategoriaTipoReceptorId = MapTipoDocAfipToCategoriaId(cabecera.tipoDocRec);


        comprobanteResult.TipoCodigoAutorizacionId = ComprobanteAnalysisHelper.MapLetraToTipoCodAutId(cabecera.tipoCodAut); // Usar helper inverso
        comprobanteResult.CodigoAutorizacion = cabecera.codAut.ToString();
        comprobanteResult.ValidacionQR = true;

        return true; // Se procesó exitosamente desde QR
    }
}
