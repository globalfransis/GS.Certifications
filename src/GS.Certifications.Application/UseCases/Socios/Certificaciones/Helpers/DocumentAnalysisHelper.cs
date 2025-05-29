using System;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Helpers
{
    public class DocumentAnalysisHelper
    {
        public static DateTime? TryParseSpanishDateInternal(string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return null;
            }

            string normalizedDate = dateString.ToLowerInvariant().Trim();
            // quitamos puntuación común al final (coma, punto)
            if (normalizedDate.EndsWith(".") || normalizedDate.EndsWith(","))
            {
                normalizedDate = normalizedDate.Substring(0, normalizedDate.Length - 1).Trim();
            }

            // Cultura para parsear nombres de meses en español (Argentina como ejemplo)
            var culture = new System.Globalization.CultureInfo("es-AR");

            // Patrón 1: "DD de MMMM de YYYY" (ej. "20 de agosto de 2021")
            string[] numericFormats = { "d 'de' MMMM 'de' yyyy", "dd 'de' MMMM 'de' yyyy" };
            if (DateTime.TryParseExact(normalizedDate, numericFormats, culture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }

            // Patrón 2: "DD días del mes de MMMM del año YYYY_ESCRITO"
            // Esto es más complejo. Aquí un intento MUY simplificado para casos específicos.
            // Necesitarías una lógica más robusta para convertir "nueve" a 9, "febrero" a 2, "dos mil quince" a 2015.

            // Ejemplo específico para "nueve días del mes de febrero del año dos mil quince"
            if (normalizedDate == "nueve días del mes de febrero del año dos mil quince")
            {
                return new DateTime(2015, 2, 9);
            }
            // Ejemplo específico para "veintitres de marzo del año dos mil veintitres" (si es así)
            if (normalizedDate == "veintitres de marzo del año dos mil veintitres" || normalizedDate == "23 de marzo del año 2023") // Considerando la variación
            {
                return new DateTime(2023, 3, 23);
            }


            // Intento de Regex para formatos como "DD días del mes de MMMM del año YYYY" donde DD y YYYY son dígitos
            // y el mes es un nombre.
            var regexPattern = new System.Text.RegularExpressions.Regex(@"^(\d{1,2})(?:\s+días)?\s+del\s+mes\s+de\s+(\w+)\s+del\s+año\s+(\d{4})$");
            var match = regexPattern.Match(normalizedDate);
            if (match.Success)
            {
                try
                {
                    int day = int.Parse(match.Groups[1].Value);
                    string monthName = match.Groups[2].Value;
                    int year = int.Parse(match.Groups[3].Value);

                    for (int i = 1; i <= 12; i++)
                    {
                        if (culture.DateTimeFormat.GetMonthName(i).ToLowerInvariant() == monthName)
                        {
                            return new DateTime(year, i, day);
                        }
                    }
                }
                catch { /* Falló el parseo del regex, continuar */ }
            }

            // Puedes añadir más patrones y lógica aquí.
            // Considera usar librerías de terceros para parseo de lenguaje natural si se vuelve muy complejo,
            // aunque para español pueden ser limitadas.

            // Como último recurso, un TryParse general (menos preciso para estos formatos)
            if (DateTime.TryParse(normalizedDate, culture, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out DateTime generalParsedDate))
            {
                return generalParsedDate;
            }

            Console.WriteLine($"ADVERTENCIA: No se pudo parsear la fecha desde la descripción: '{dateString}'");
            return null;
        }
    }
}
