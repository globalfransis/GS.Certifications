using Azure.AI.DocumentIntelligence;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GS.Certifications.Domain.Entities.Percepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies;

public class DefaultPerceptionStrategy : IComprobantePerceptionStrategy
{
    private readonly ICertificationsDbContext _dbContext;
    public DefaultPerceptionStrategy(ICertificationsDbContext context) { _dbContext = context; }

    public async Task PopulatePerceptionsAsync(AnalisysContext context)
    {
        Log.Logger.Debug("Ejecutando DefaultPerceptionStrategy...");
        var perceptionList = new List<IComprobantePercepcionAnalysisResult>();

        var perceptionsField = context.ExtractedFields?.GetValueOrDefault("Percepciones");
        if (perceptionsField != null)
        {
            var percepcionesEnBaseDeDatos = await _dbContext.Percepciones
                .Where(p => p.CompanyId == context.Parameters.CompanyId)
                .Select(p => new { p.Descripcion, p.Id, p.PercepcionTipoId })
                .ToListAsync();

            foreach (var item in perceptionsField.ValueList)
            {
                var nuevaPercepcionAnalysisResult = new ComprobantePercepcionAnalysisResult();
                var itemFields = item.ValueDictionary;

                if (itemFields.TryGetValue("Descripcion", out DocumentField descripcionField))
                {
                    nuevaPercepcionAnalysisResult.Descripcion = descripcionField.ValueString;

                    // 1. Normalización de la descripción
                    string descripcionNormalizada = ComprobanteAnalysisHelper.NormalizarDescripcionPercepcion(nuevaPercepcionAnalysisResult.Descripcion);

                    // 2. Priorización de criterios de búsqueda
                    var percepcionEncontrada = percepcionesEnBaseDeDatos.FirstOrDefault(p =>
                        string.Compare(ComprobanteAnalysisHelper.NormalizarDescripcionPercepcion(p.Descripcion), descripcionNormalizada, StringComparison.InvariantCultureIgnoreCase) == 0 // Coincidencia exacta
                    );

                    if (percepcionEncontrada == null && itemFields.TryGetValue("Alicuota", out DocumentField alicuotaPercepcionField))
                    {
                        var alicuota = ComprobanteAnalysisHelper.ParseNumberFromContent(alicuotaPercepcionField);
                        if (alicuota.HasValue)
                        {
                            // Buscamos por coincidencia parcial de la descripción normalizada Y la alícuota
                            percepcionEncontrada = percepcionesEnBaseDeDatos.FirstOrDefault(p =>
                                descripcionNormalizada.Contains(ComprobanteAnalysisHelper.NormalizarDescripcionPercepcion(p.Descripcion)) || ComprobanteAnalysisHelper.NormalizarDescripcionPercepcion(p.Descripcion).Contains(descripcionNormalizada)
                            );
                        }
                    }

                    // Si aún no se encuentra, buscamos solo por coincidencia parcial (menos prioritario)
                    if (percepcionEncontrada == null)
                    {
                        percepcionEncontrada = percepcionesEnBaseDeDatos.FirstOrDefault(p =>
                            descripcionNormalizada.Contains(ComprobanteAnalysisHelper.NormalizarDescripcionPercepcion(p.Descripcion)) || ComprobanteAnalysisHelper.NormalizarDescripcionPercepcion(p.Descripcion).Contains(descripcionNormalizada)
                        );
                    }

                    if (percepcionEncontrada != null)
                    {
                        nuevaPercepcionAnalysisResult.PercepcionId = percepcionEncontrada.Id;
                        nuevaPercepcionAnalysisResult.TipoId = percepcionEncontrada.PercepcionTipoId;
                    }
                    else
                    {
                        Console.WriteLine($"Advertencia: No se encontró la Percepción para la descripción '{nuevaPercepcionAnalysisResult.Descripcion}'.");
                    }
                }

                if (itemFields.TryGetValue("Importe", out DocumentField importeField))
                {
                    nuevaPercepcionAnalysisResult.ImporteTotal = ComprobanteAnalysisHelper.ParseNumberFromContent(importeField) ?? 0;
                }

                if (itemFields.TryGetValue("Alicuota", out DocumentField alicuotaField))
                {
                    nuevaPercepcionAnalysisResult.Alicuota = ComprobanteAnalysisHelper.ParseNumberFromContent(alicuotaField) ?? 0;
                }

                perceptionList.Add(nuevaPercepcionAnalysisResult);
            }
        }
        context.ResultInProgress.Percepciones = perceptionList;

        context.ResultInProgress.ImportePercepcionIVA = perceptionList.Where(p => p.TipoId == PercepcionTipo.IVA).Sum(p => p.ImporteTotal ?? 0);
        context.ResultInProgress.ImportePercepcionIIBB = perceptionList.Where(p => p.TipoId == PercepcionTipo.IIBB).Sum(p => p.ImporteTotal ?? 0);
        context.ResultInProgress.ImportePercepcionMunicipal = perceptionList.Where(p => p.TipoId == PercepcionTipo.MUNICIPALES).Sum(p => p.ImporteTotal ?? 0);

        Log.Logger.Debug("DefaultPerceptionStrategy finalizada. Percepciones procesadas: {Count}", perceptionList.Count);
    }
}
