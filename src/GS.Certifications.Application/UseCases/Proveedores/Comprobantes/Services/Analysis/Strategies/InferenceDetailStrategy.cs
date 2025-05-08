using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

public class InferenceDetailStrategy : IComprobanteDetailStrategy
{
    public Task PopulateDetailsAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando InferenceDetailStrategy...");
        var detailsList = new List<IComprobanteDetalleAnalysisResult>();
        decimal toleranciaBonificacion = 0.01m;

        decimal? subtotalParsed = ComprobanteAnalysisHelper.ParseNumberFromContent(context.ExtractedFields.GetValueOrDefault("Subtotal"));
        decimal? totalParsed = ComprobanteAnalysisHelper.ParseNumberFromContent(context.ExtractedFields.GetValueOrDefault("Total"));

        if (!subtotalParsed.HasValue)
        {
            Log.Logger.Warning("InferenceDetailStrategy: No se pudo extraer un Subtotal válido. No se pueden inferir detalles.");
            context.ResultInProgress.Detalles = detailsList;
            return Task.CompletedTask;
        }

        var detallePrincipal = new ComprobanteDetalleAnalysisResult
        {
            Cantidad = 1,
            Detalle = "Detalle inferido (basado en Subtotal)",
            PrecioUnitario = subtotalParsed.Value,
            Subtotal = subtotalParsed.Value,
            ImporteBonificacion = 0
        };
        detailsList.Add(detallePrincipal);

        if (totalParsed.HasValue)
        {
            decimal diferencia = subtotalParsed.Value - totalParsed.Value;

            if (diferencia > toleranciaBonificacion)
            {
                Log.Logger.Information("InferenceDetailStrategy: Se infiere una bonificación de {BonificacionAmount} (Subtotal: {Subtotal}, Total: {Total})", diferencia, subtotalParsed.Value, totalParsed.Value);


                var detalleBonificacion = new ComprobanteDetalleAnalysisResult
                {
                    Cantidad = 1,
                    Detalle = "Bonificación inferida (basado en Subtotal)",
                    PrecioUnitario = 0,
                    Subtotal = diferencia,
                    ImporteBonificacion = diferencia
                };

                detailsList.Add(detalleBonificacion);

                context.ResultInProgress.ImporteBonificacion = (context.ResultInProgress.ImporteBonificacion ?? 0) + diferencia;
            }
            else if (diferencia < -toleranciaBonificacion)
            {
                Log.Logger.Warning("InferenceDetailStrategy: El Total ({Total}) es significativamente MAYOR que el Subtotal ({Subtotal}). Verificar cargos adicionales.", totalParsed.Value, subtotalParsed.Value);
            }
            else
            {
                Log.Logger.Debug("InferenceDetailStrategy: Diferencia entre Subtotal ({Subtotal}) y Total ({Total}) dentro de la tolerancia ({Tolerancia}). No se infiere bonificación.", subtotalParsed.Value, totalParsed.Value, toleranciaBonificacion);
            }
        }
        else
        {
            Log.Logger.Warning("InferenceDetailStrategy: No se pudo extraer el Total. No se puede inferir bonificación.");
        }

        context.ResultInProgress.Detalles = detailsList;

        Log.Logger.Debug("InferenceDetailStrategy finalizada. Items inferidos: {Count}", detailsList.Count);
        return Task.CompletedTask;
    }
}
