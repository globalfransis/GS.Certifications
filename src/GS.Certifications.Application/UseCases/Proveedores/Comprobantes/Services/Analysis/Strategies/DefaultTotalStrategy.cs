using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers;
using Serilog;
using GS.Certifications.Domain.Entities.Impuestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

public class DefaultTotalStrategy : IComprobanteTotalStrategy
{
    public Task PopulateTotalsAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando DefaultTotalStrategy...");
        var comprobanteResult = context.ResultInProgress;
        var extractedFields = context.ExtractedFields;

        // Calcular Neto: Suma de subtotales de items MENOS bonificaciones globales
        comprobanteResult.ImporteNeto = ComprobanteAnalysisHelper.ParseNumberFromContent(extractedFields.GetValueOrDefault("Subtotal")) ?? CalcularImporteNeto(comprobanteResult);

        if (comprobanteResult.ImporteTotal == null || comprobanteResult.ImporteTotal == 0)
        {
            comprobanteResult.ImporteTotal = ComprobanteAnalysisHelper.ParseNumberFromContent(extractedFields.GetValueOrDefault("Total")) ?? CalcularImporteTotal(comprobanteResult);
        }


        // --- INFERENCIA DE DATOS FALTANTES ---
        // Si el total sigue siendo null o 0
        if (comprobanteResult.ImporteTotal == null || comprobanteResult.ImporteTotal == 0)
        {
            var impuestoIVA = comprobanteResult.Impuestos.OrderByDescending(i => i.AlicuotaValor).FirstOrDefault(i => i.TipoId == ImpuestoTipo.IVA);
            if (impuestoIVA != null)
            {
                comprobanteResult.ImporteTotal = comprobanteResult.ImporteNeto * (1 + impuestoIVA.AlicuotaValor);
            }
        }
        // Si el neto sigue siendo null o 0
        if (comprobanteResult.ImporteNeto == null || comprobanteResult.ImporteNeto == 0)
        {
            var impuestoIVA = comprobanteResult.Impuestos.OrderByDescending(i => i.AlicuotaValor).FirstOrDefault(i => i.TipoId == ImpuestoTipo.IVA);
            if (impuestoIVA != null)
            {
                comprobanteResult.ImporteNeto = comprobanteResult.ImporteTotal / (1 + impuestoIVA.AlicuotaValor);
            }
        }
        // --- FIN INFERENCIA DATOS FALTANTES --- 


        Log.Logger.Debug("DefaultTotalStrategy finalizada. Neto={Neto}, Total={Total}", comprobanteResult.ImporteNeto, comprobanteResult.ImporteTotal);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Calcula el importe neto esperado sumando los subtotales de los detalles y restando bonificaciones globales.
    /// </summary>
    private decimal CalcularImporteNeto(ComprobanteAnalysisResult comprobante)
    {
        // La suma de los ítems sin impuestos menos las bonificaciones es igual al total neto
        decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario ?? 0 * d.Cantidad ?? 0);
        decimal totalBonificaciones = comprobante.Detalles.Sum(d => d.ImporteBonificacion ?? 0);
        decimal totalNetoEsperado = sumaItemsSinImpuestos - totalBonificaciones;

        return totalNetoEsperado;
    }

    /// <summary>
    /// Calcula el importe total esperado sumando neto, impuestos y percepciones.
    /// </summary>
    private decimal CalcularImporteTotal(ComprobanteAnalysisResult comprobante)
    {
        decimal sumaItemsSinImpuestos = comprobante.Detalles.Sum(d => d.PrecioUnitario ?? 0 * d.Cantidad ?? 0);

        return Math.Abs(comprobante.ImporteTotal ?? 0 - sumaItemsSinImpuestos);
    }
}
