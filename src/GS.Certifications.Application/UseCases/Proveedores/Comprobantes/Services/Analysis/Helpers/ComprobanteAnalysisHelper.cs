using Azure.AI.DocumentIntelligence;
using GS.Certifications.Domain.Entities.Comprobantes;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Helpers
{
    // Clase estática para métodos auxiliares de análisis
    public static class ComprobanteAnalysisHelper
    {
        // --- Métodos movidos de ComprobanteService ---

        public static string GetBase64CodeFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            Uri uri = new Uri(url);
            string queryString = uri.Query;

            var queryParams = System.Web.HttpUtility.ParseQueryString(queryString);
            string base64Code = queryParams["p"];

            return base64Code;
        }

        public static string DecodeBase64(string base64Code)
        {
            if (string.IsNullOrEmpty(base64Code)) return string.Empty;

            var bytebase64Bytes = Convert.FromBase64String(base64Code);
            return Encoding.UTF8.GetString(bytebase64Bytes);
        }

        public static string NormalizarDescripcionPercepcion(string descripcion)
        {
            if (string.IsNullOrEmpty(descripcion)) return string.Empty;

            string normalizada = descripcion.Trim();
            normalizada = normalizada.ToUpper();
            normalizada = normalizada.Replace(" ", "");
            normalizada = normalizada.Replace("-", "");
            normalizada = normalizada.Replace("/", "");
            normalizada = normalizada.Replace(".", "");
            normalizada = normalizada.Replace(",", "");

            // Abreviaturas y variaciones específicas de percepciones
            normalizada = normalizada.Replace("PER/RETDE", "PERCEPCIONRETENCIONDE");
            normalizada = normalizada.Replace("PER/RET", "PERCEPCIONRETENCION");
            normalizada = normalizada.Replace("PER", "PERCEPCION");
            normalizada = normalizada.Replace("RET", "RETENCION");
            normalizada = normalizada.Replace("GANANCIAS", "GANANCIA");
            normalizada = normalizada.Replace("IIBB", "INGRESOSBRUTOS");
            normalizada = normalizada.Replace("IMPUESTOALASGANANCIAS", "IMPUESTOAGANANCIAS");
            // *** IMPORTANTE:  Considerar el orden de los reemplazos ***

            return normalizada;
        }

        public static string NormalizarDescripcionImpuesto(string descripcion)
        {
            if (string.IsNullOrEmpty(descripcion)) return string.Empty;

            string normalizada = descripcion.Trim();
            normalizada = normalizada.ToUpper();
            normalizada = normalizada.Replace(" ", "");
            normalizada = normalizada.Replace("-", "");
            normalizada = normalizada.Replace("/", "");
            normalizada = normalizada.Replace("%", "");
            normalizada = normalizada.Replace(".", "");
            normalizada = normalizada.Replace(",", "");

            normalizada = normalizada.Replace("IVA", "IMPUESTOALVALORAGREGADO");
            normalizada = normalizada.Replace("Iva", "IMPUESTOALVALORAGREGADO");
            normalizada = normalizada.Replace("iva", "IMPUESTOALVALORAGREGADO");
            normalizada = normalizada.Replace("I.V.A.", "IMPUESTOALVALORAGREGADO");
            normalizada = normalizada.Replace("I.V.A", "IMPUESTOALVALORAGREGADO");
            normalizada = normalizada.Replace("IIBB", "IMPUESTOINGRESOSBRUTOS");
            normalizada = normalizada.Replace("GAN.", "IMPUESTOALASGANANCIAS");
            normalizada = normalizada.Replace("IMPUESTOSINTERNOS", "IMPUESTOINTERNO");
            normalizada = normalizada.Replace("IMPUESTOSMUNICIPALES", "IMPUESTOOMUNICIPAL");
            normalizada = normalizada.Replace("IMPUESTOMUNICIPALES", "IMPUESTOOMUNICIPAL");
            normalizada = normalizada.Replace("IMPUESTOMUNICIPIO", "IMPUESTOOMUNICIPAL");
            normalizada = normalizada.Replace("IMPUESTOS", "IMPUESTO");
            normalizada = normalizada.Replace("INGRESOSBRUTOS", "INGRESOSBRUTO");
            normalizada = normalizada.Replace("GANANCIAS", "GANANCIA");

            normalizada = normalizada.Replace("VALORAGREGADO", "VALORAGREGADO");
            normalizada = normalizada.Replace("V.A.", "VALORAGREGADO");


            return normalizada;
        }

        public static decimal? ParseNumberFromContent(dynamic field)
        {
            if (field?.Content != null)
            {
                string content = field.Content.ToString();
                return ConvertirNumero(content);
            }

            return null;
        }

        private static decimal? ConvertirNumero(string numeroStr)
        {
            numeroStr = Regex.Replace(numeroStr, "[^0-9.,-]", "");
            numeroStr = numeroStr.Trim();

            MatchCollection puntos = Regex.Matches(numeroStr, @"\.");
            MatchCollection comas = Regex.Matches(numeroStr, @",");

            if (puntos.Count > 0 && comas.Count > 0)
            {
                if (comas[comas.Count - 1].Index > puntos[puntos.Count - 1].Index)
                {
                    numeroStr = numeroStr.Replace(".", "").Replace(",", ".");
                }
                else
                {
                    numeroStr = numeroStr.Replace(",", "");
                }
            }
            else if (comas.Count > 0)
            {
                numeroStr = numeroStr.Replace(",", ".");
            }

            if (double.TryParse(numeroStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultado))
            {
                return Convert.ToDecimal(resultado);
            }

            return null; // Devuelve null si no se pudo convertir
        }

        public static decimal? ParseAlicuotaIVA(DocumentField alicuotaIVAField)
        {
            if (alicuotaIVAField?.Content != null)
            {
                string alicuotaString = alicuotaIVAField.Content.Trim();

                alicuotaString = alicuotaString.Replace(",", ".").Replace(" ", "").Replace("%", "");

                if (decimal.TryParse(alicuotaString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal alicuota))
                {
                    return alicuota;
                }
                else
                {
                    Console.WriteLine($"Error: No se pudo convertir la alícuota IVA '{alicuotaIVAField.ValueString}' a decimal.");
                    return null;
                }
            }
            return null;
        }

        public static string MapTipoCodAutIdToLetra(short tipoCodAutId)
        {
            // Asumiendo que CodigoAutorizacionTipo es una clase estática con constantes short
            return tipoCodAutId switch
            {
                CodigoAutorizacionTipo.CAE => "E",
                CodigoAutorizacionTipo.CAEA => "A",
                CodigoAutorizacionTipo.CAI => "I", // Verificar código correcto para CAI
                _ => throw new InvalidOperationException($"Tipo de Código de Autorización ID desconocido: {tipoCodAutId}")
            };
        }

        public static short MapLetraToTipoCodAutId(string letra)
        {
            return letra?.ToUpper() switch
            {
                "E" => CodigoAutorizacionTipo.CAE,
                "A" => CodigoAutorizacionTipo.CAEA,
                "I" => CodigoAutorizacionTipo.CAI,
                _ => CodigoAutorizacionTipo.CAE // O lanzar error?
            };
        }

    }
}
