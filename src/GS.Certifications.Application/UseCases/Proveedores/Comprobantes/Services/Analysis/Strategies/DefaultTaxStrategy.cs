using Azure.AI.DocumentIntelligence;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GS.Certifications.Domain.Entities.Impuestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

public class DefaultTaxStrategy : IComprobanteTaxStrategy
{
    private readonly ICertificationsDbContext _context;
    public DefaultTaxStrategy(ICertificationsDbContext context) { _context = context; }

    public async Task PopulateTaxesAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando DefaultTaxStrategy...");
        var taxList = new List<IComprobanteImpuestoAnalysisResult>();
        var taxesField = context.ExtractedFields?.GetValueOrDefault("Impuestos");
        decimal toleranciaAlicuota = 0.001m; // tolerancia para comparar tasas (0.1%)

        if (taxesField == null || taxesField.FieldType != DocumentFieldType.List)
        {
            Log.Logger.Warning("Campo 'Impuestos' no encontrado o no es una lista en el documento.");
            // handle fallback for IVA calculation if necessary and possible
            HandleFallbackIVACalculation(context);
            return;
        }

        var impuestosEnBD = await _context.Impuestos.AsNoTracking()
            .Where(i => i.CompanyId == context.Parameters.CompanyId)
            .Select(i => new
            {
                i.Id,
                i.Descripcion,
                i.TipoId,
                AlicuotaValor = i.Alicuota != null ? i.Alicuota.Valor : null,
                ValorFijo = i.Valor
            })
            .ToListAsync();

        // pre-normalizamos descripciones de BD para eficiencia
        var impuestosBDNormalizados = impuestosEnBD.Select(i => new
        {
            Data = i,
            DescripcionNormalizada = ComprobanteAnalysisHelper.NormalizarDescripcionImpuesto(i.Descripcion ?? string.Empty)
        }).ToList();


        foreach (var item in taxesField.ValueList)
        {
            if (item.FieldType != DocumentFieldType.Dictionary) continue;

            var nuevoImpuestoAnalysisResult = new ComprobanteImpuestoAnalysisResult();
            var itemFields = item.ValueDictionary;

            nuevoImpuestoAnalysisResult.ImporteTotal = ComprobanteAnalysisHelper.ParseNumberFromContent(itemFields.GetValueOrDefault("Monto"));
            string descripcionOriginal = itemFields.GetValueOrDefault("Descripcion")?.ValueString;
            decimal? alicuotaExtraida = ComprobanteAnalysisHelper.ParseNumberFromContent(itemFields.GetValueOrDefault("Alicuota"));

            // si no hay importe o descripción, probablemente no sea un impuesto válido extraído
            if ((nuevoImpuestoAnalysisResult.ImporteTotal ?? 0) == 0 || string.IsNullOrWhiteSpace(descripcionOriginal))
            {
                Log.Logger.Debug("Item de impuesto omitido por falta de importe o descripción.");
                continue;
            }

            nuevoImpuestoAnalysisResult.Descripcion = descripcionOriginal;
            string descripcionNormalizada = ComprobanteAnalysisHelper.NormalizarDescripcionImpuesto(descripcionOriginal);

            dynamic impuestoEncontrado = null;
            string metodoMatch = "Ninguno";

            // --- Cascada de Matching ---

            // 1. TODO: matching por Alias
            // impuestoEncontrado = _aliasService.FindImpuestoByAlias(descripcionNormalizada, context.Parameters.CompanyId);
            // if (impuestoEncontrado != null) { metodoMatch = "Alias"; }

            // 2. matching priorizando alícuota (si no se encontró por alias y hay alícuota)
            if (impuestoEncontrado == null && alicuotaExtraida.HasValue)
            {
                var candidatosPorTasa = impuestosBDNormalizados
                    .Where(i => i.Data.AlicuotaValor.HasValue && Math.Abs(i.Data.AlicuotaValor.Value - alicuotaExtraida.Value) < toleranciaAlicuota)
                    .ToList();

                if (candidatosPorTasa.Count == 1)
                {
                    impuestoEncontrado = candidatosPorTasa.First().Data;
                    metodoMatch = "Alicuota Unica";
                }
                else if (candidatosPorTasa.Count > 1)
                {
                    var tipoInferido = InferTipoDesdeDescripcion(descripcionNormalizada);
                    var mejorCandidato = candidatosPorTasa
                        .OrderByDescending(c => c.Data.TipoId == tipoInferido ? 1 : 0)
                        .ThenByDescending(c => CalculateDescriptionSimilarity(descripcionNormalizada, c.DescripcionNormalizada))
                        .FirstOrDefault();
                    if (mejorCandidato != null)
                    {
                        impuestoEncontrado = mejorCandidato.Data;
                        metodoMatch = "Alicuota + Desambiguacion";
                    }
                }
            }

            // 3. matching por Descripción Exacta Normalizada (si no se encontró antes)
            if (impuestoEncontrado == null)
            {
                var matchExacto = impuestosBDNormalizados.FirstOrDefault(i =>
                    string.Equals(i.DescripcionNormalizada, descripcionNormalizada, StringComparison.OrdinalIgnoreCase)); // OrdinalIgnoreCase es más rápido para comparar igualdad normalizada
                if (matchExacto != null)
                {
                    impuestoEncontrado = matchExacto.Data;
                    metodoMatch = "Descripcion Exacta Normalizada";
                }
            }

            // 4. Matching por Descripción Parcial + Alícuota (si no se encontró antes y hay alícuota)
            if (impuestoEncontrado == null && alicuotaExtraida.HasValue)
            {
                var matchParcialTasa = impuestosBDNormalizados.FirstOrDefault(i =>
                   (i.DescripcionNormalizada.Contains(descripcionNormalizada) || descripcionNormalizada.Contains(i.DescripcionNormalizada)) &&
                   i.Data.AlicuotaValor.HasValue && Math.Abs(i.Data.AlicuotaValor.Value - alicuotaExtraida.Value) < toleranciaAlicuota);
                if (matchParcialTasa != null)
                {
                    impuestoEncontrado = matchParcialTasa.Data;
                    metodoMatch = "Descripcion Parcial + Alicuota";
                }
            }


            // 5. Matching por Descripción Parcial (último recurso)
            if (impuestoEncontrado == null)
            {
                var matchParcial = impuestosBDNormalizados
                     .Where(i => i.DescripcionNormalizada.Contains(descripcionNormalizada) || descripcionNormalizada.Contains(i.DescripcionNormalizada))
                     .OrderByDescending(c => CalculateDescriptionSimilarity(descripcionNormalizada, c.DescripcionNormalizada)) // Ordenar por similitud
                     .FirstOrDefault();
                if (matchParcial != null)
                {
                    impuestoEncontrado = matchParcial.Data;
                    metodoMatch = "Descripcion Parcial";
                }
            }

            // asignación o manejo de no match
            if (impuestoEncontrado != null)
            {
                nuevoImpuestoAnalysisResult.ImpuestoId = impuestoEncontrado.Id;
                nuevoImpuestoAnalysisResult.TipoId = impuestoEncontrado.TipoId;
                Log.Logger.Information("Impuesto '{DescExtr}' mapeado a ImpuestoId {ImpId} ('{DescDB}') usando método [{Metodo}]",
                                       descripcionOriginal, impuestoEncontrado.Id, impuestoEncontrado.Descripcion, metodoMatch);
            }
            else
            {
                // **** CASO: NO HUBO MATCH ****
                Log.Logger.Warning("MATCH NO ENCONTRADO para impuesto extraído: Desc='{DescExtr}', Monto={MontoExtr}, Alicuota={AlicuotaExtr}. Se añadirá sin ID de impuesto.",
                                   descripcionOriginal, nuevoImpuestoAnalysisResult.ImporteTotal, alicuotaExtraida);

                // se podría añadir de todas formas pero sin ImpuestoId/TipoId
                // nuevoImpuestoAnalysisResult.ImpuestoId = null;
                // nuevoImpuestoAnalysisResult.TipoId = null;

                // alternativa: marcarlo como desconocido (requiere definir un TipoId para 'Desconocido')
                // nuevoImpuestoAnalysisResult.TipoId = ImpuestoTipo.DESCONOCIDO;

                // otra alternativa: añadir a una lista separada para revisión manual
                // context.ResultInProgress.AddUnmatchedTax(nuevoImpuestoAnalysisResult);
                // continue; // No añadir a la lista principal taxList
            }
            taxList.Add(nuevoImpuestoAnalysisResult);
        }

        context.ResultInProgress.Impuestos = taxList;

        CalculateTotalsByType(context);

        Log.Logger.Debug("DefaultTaxStrategy finalizada. Impuestos procesados: {Count}", taxList.Count);
    }

    // --- Funciones Helper (Implementar lógica) ---
    private short? InferTipoDesdeDescripcion(string descripcionNormalizada)
    {
        if (descripcionNormalizada.Contains("iva")) return ImpuestoTipo.IVA;
        if (descripcionNormalizada.Contains("perc") || descripcionNormalizada.Contains("ret") || descripcionNormalizada.Contains("iibb") || descripcionNormalizada.Contains("ingresos brutos")) return ImpuestoTipo.PROVINCIAL;
        if (descripcionNormalizada.Contains("int") || descripcionNormalizada.Contains("interno")) return ImpuestoTipo.INTERNO;
        // Añadir más reglas según sea necesario
        return null;
    }

    private double CalculateDescriptionSimilarity(string desc1, string desc2)
    {
        // Implementar un cálculo de similitud (ej. Levenshtein, Jaccard, o simple conteo de palabras comunes)
        // Devuelve un valor entre 0 y 1 (o similar) donde mayor es más similar.
        // Ejemplo muy básico:
        if (string.IsNullOrEmpty(desc1) || string.IsNullOrEmpty(desc2)) return 0;
        if (desc1 == desc2) return 1;
        if (desc1.Contains(desc2) || desc2.Contains(desc1)) return 0.5; // Simple contains
        return 0; // No similar
    }

    private void CalculateTotalsByType(AnalisysContext context)
    {
        var taxList = context.ResultInProgress.Impuestos ?? new List<IComprobanteImpuestoAnalysisResult>();

        context.ResultInProgress.ImporteIVA = taxList
            .Where(i => i.TipoId == ImpuestoTipo.IVA)
            .Sum(i => i.ImporteTotal ?? 0);

        context.ResultInProgress.ImporteImpuestosInternos = taxList
            .Where(i => i.TipoId == ImpuestoTipo.INTERNO)
            .Sum(i => i.ImporteTotal ?? 0);

        context.ResultInProgress.ImporteOtrosTributosProv = taxList
            .Where(i => i.TipoId == ImpuestoTipo.PROVINCIAL)
            .Sum(i => i.ImporteTotal ?? 0);


        // revisar el Fallback para IVA: solo aplicarlo si ImporteIVA es 0 y hay diferencia Total-Neto
        if (context.ResultInProgress.ImporteIVA == 0 && taxList.All(t => t.TipoId != ImpuestoTipo.IVA)) // si no se mapeó ningún IVA explícitamente
        {
            HandleFallbackIVACalculation(context);
        }
    }

    private void HandleFallbackIVACalculation(AnalisysContext context)
    {
        // TODO: esta lógica es riesgosa porque asume que TODA la diferencia es IVA, podrían ser otros impuestos
        Log.Logger.Debug("Intentando cálculo fallback para IVA...");
        try
        {
            // Asegurarse que estas funciones helper sean robustas y correctas
            var importeNeto = ComprobanteAnalysisHelper.ParseNumberFromContent(context.ExtractedFields.GetValueOrDefault("Subtotal"))
                              ?? CalcularImporteNetoFallback(context.ResultInProgress);
            var importeTotal = ComprobanteAnalysisHelper.ParseNumberFromContent(context.ExtractedFields.GetValueOrDefault("Total"))
                              ?? CalcularImporteTotalFallback(context.ResultInProgress); // O usar el ImporteTotal ya parseado

            if (importeTotal > importeNeto && importeNeto > 0) // Solo si hay diferencia positiva y neto es positivo
            {
                decimal diferencia = importeTotal - importeNeto - (context.ResultInProgress.ImporteImpuestosInternos ?? 0) - (context.ResultInProgress.ImporteOtrosTributosProv ?? 0);
                if (diferencia > 0.01m) // Evitar asignar por redondeos mínimos
                {
                    Log.Logger.Information("Cálculo Fallback: Asignando {Diferencia} a ImporteIVA (Total: {Total}, Neto: {Neto}, OtrosImp: {Otros})",
                                           diferencia, importeTotal, importeNeto, (context.ResultInProgress.ImporteImpuestosInternos ?? 0) + (context.ResultInProgress.ImporteOtrosTributosProv ?? 0));
                    context.ResultInProgress.ImporteIVA = diferencia;
                    // Opcional: Crear un item de impuesto IVA inferido si la lista está vacía?
                }
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error durante el cálculo fallback de IVA.");
        }
    }

    // --- Funciones Fallback (Revisar y corregir lógica si se usan) ---
    // La implementación de estas funciones en tu código original parecía tener errores.
    private decimal CalcularImporteNetoFallback(ComprobanteAnalysisResult comprobante)
    {
        if (comprobante?.Detalles == null || !comprobante.Detalles.Any()) return 0;
        // Corrección: Manejar nulls y precedencia de operadores
        decimal sumaItems = comprobante.Detalles.Sum(d => (d.PrecioUnitario ?? 0m) * (d.Cantidad ?? 1m)); // Asumir Cantidad 1 si es null? OJO.
        decimal totalBonificaciones = comprobante.Detalles.Sum(d => d.ImporteBonificacion ?? 0m) + (comprobante.ImporteBonificacion ?? 0m); // Incluir bonificación global
        return sumaItems - totalBonificaciones;
    }

    private decimal CalcularImporteTotalFallback(ComprobanteAnalysisResult comprobante)
    {
        // Esta función probablemente debería devolver el comprobante.ImporteTotal si existe,
        // o intentar calcularlo sumando Neto + Impuestos Mapeados si ImporteTotal no se extrajo.
        // La versión original `Math.Abs(comprobante.ImporteTotal ?? 0 - sumaItemsSinImpuestos)` no calcula un total esperado.
        return comprobante.ImporteTotal ?? 0; // Devolver el total extraído si existe, sino 0 (o lanzar error?)
    }

}
