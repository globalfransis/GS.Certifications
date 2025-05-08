using Azure.AI.DocumentIntelligence;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GS.Certifications.Domain.Entities.Impuestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

public class DefaultDetailStrategy : IComprobanteDetailStrategy
{
    private readonly ICertificationsDbContext _context;
    public DefaultDetailStrategy(ICertificationsDbContext context) { _context = context; }

    public async Task PopulateDetailsAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando DefaultDetailStrategy...");
        var detailsList = new List<IComprobanteDetalleAnalysisResult>();
        var itemsField = context.ExtractedFields?.GetValueOrDefault("Items");

        var discountsField = context.ExtractedFields?.GetValueOrDefault("Descuentos");

        if (itemsField != null && itemsField.ValueList != null)
        {
            var unidadesMedida = await _context.UnidadMedidas.AsNoTracking().Select(u => new { u.Idm, u.CodigoAFIP, u.Descripcion }).ToListAsync();
            var impuestosIvaBD = await _context.Impuestos.AsNoTracking()
                                       .Include(i => i.Alicuota)
                                       .Where(i => i.TipoId == ImpuestoTipo.IVA && i.CompanyId == context.Parameters.CompanyId)
                                       .ToListAsync();

            foreach (var field in itemsField.ValueList)
            {
                var detalle = new ComprobanteDetalleAnalysisResult
                {
                    Subtotal = ComprobanteAnalysisHelper.ParseNumberFromContent(field.ValueDictionary.GetValueOrDefault("Importe")),
                    Detalle = field.ValueDictionary.GetValueOrDefault("Detalle")?.ValueString,
                    PrecioUnitario = ComprobanteAnalysisHelper.ParseNumberFromContent(field.ValueDictionary.GetValueOrDefault("PrecioUnitario")),
                    Cantidad = int.TryParse(field.ValueDictionary.GetValueOrDefault("Cantidad")?.Content, out int result) ? result : 1,
                    ImporteBonificacion = 0
                };

                // Deducción de precio unitario: si PrecioUnitario es null y Subtotal es distinto de null -> PrecioUnitario = Subtotal / Cantidad
                detalle.PrecioUnitario ??= (detalle.Subtotal ?? 0) / detalle.Cantidad;

                if (field.ValueDictionary.TryGetValue("Unidad", out DocumentField unidadField) && !string.IsNullOrEmpty(unidadField.ValueString))
                {
                    var unidadBuscadaIdm = unidadesMedida.FirstOrDefault(u => u.CodigoAFIP == unidadField.ValueString || u.CodigoAFIP.Contains(unidadField.ValueString) || unidadField.ValueString.Contains(u.CodigoAFIP))?.Idm;
                    detalle.UnidadMedidaId = unidadBuscadaIdm;
                }

                if (field.ValueDictionary.TryGetValue("AlicuotaIVA", out DocumentField alicuotaIVAField))
                {
                    decimal? alicuotaIVA = ComprobanteAnalysisHelper.ParseAlicuotaIVA(alicuotaIVAField);

                    if (alicuotaIVA.HasValue)
                    {
                        var impuestoIVA = impuestosIvaBD
                            .Where(i => i.Alicuota.Valor == alicuotaIVA && i.TipoId == ImpuestoTipo.IVA && i.CompanyId == context.Parameters.CompanyId)
                            .FirstOrDefault();

                        if (impuestoIVA is not null)
                        {
                            detalle.ImpuestoIVAId = impuestoIVA.Id;
                            detalle.Alicuota = impuestoIVA.Alicuota.Valor;
                            detalle.ImporteIVA = (decimal)detalle.Subtotal + (decimal)detalle.Subtotal * (decimal)impuestoIVA.Alicuota.Valor / 100;
                        }
                        else
                        {
                            Console.WriteLine($"Advertencia: No se encontró el Impuesto de IVA para la alícuota '{alicuotaIVA}'.");
                        }
                    }
                }

                detailsList.Add(detalle);
            }
        }

        //decimal bonificacionGlobal = 0m;
        //var discountsField = context.ExtractedFields?.GetValueOrDefault("Discounts", "Descuentos"); // Ajustar nombres
        //if (discountsField?.Type == DocumentFieldType.List)
        //{
        //    foreach (var discountItemField in discountsField.ValueList)
        //    {
        //        if (discountItemField.Type == DocumentFieldType.Dictionary)
        //        {
        //            bonificacionGlobal += ComprobanteAnalysisHelper.ParseNumberFromContent(discountItemField.ValueDictionary.GetValueOrDefault("Amount", "Importe")) ?? 0m;
        //            // Podría añadirse como un detalle negativo si se prefiere
        //        }
        //    }
        //}
        //else if (discountsField?.Type == DocumentFieldType.Currency || discountsField?.Type == DocumentFieldType.Double)
        //{
        //    bonificacionGlobal = ComprobanteAnalysisHelper.ParseNumberFromContent(discountsField) ?? 0m;
        //}
        //context.ResultInProgress.ImporteBonificacion = bonificacionGlobal; // O sumar con bonificaciones a nivel item si existen

        if (discountsField is not null && discountsField.ValueList != null)
        {
            foreach (var field in discountsField.ValueList)
            {
                var detalle = new ComprobanteDetalleAnalysisResult
                {
                    ImporteBonificacion = ComprobanteAnalysisHelper.ParseNumberFromContent(field.ValueDictionary.GetValueOrDefault("Importe")) ?? 0,
                    PrecioUnitario = 0,
                    Cantidad = 0,
                    Detalle = field.ValueDictionary.GetValueOrDefault("Descripcion")?.ValueString
                };
                detailsList.Add(detalle);
                context.ResultInProgress.ImporteBonificacion += detalle.ImporteBonificacion;
            }
        }


        context.ResultInProgress.Detalles = detailsList;

        if (context.ResultInProgress.Detalles.Count == 0)
        {
            await Task.FromResult(InferDetail(context));
        }

        Log.Logger.Debug("DefaultDetailStrategy finalizada. Items procesados: {Count}", detailsList.Count);
    }

    private Task InferDetail(AnalisysContext context)
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
