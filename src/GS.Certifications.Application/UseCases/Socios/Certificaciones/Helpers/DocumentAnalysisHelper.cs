using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Helpers;


public static class DocumentAnalysisHelper
{
    private static readonly Dictionary<string, int> SpanishNumberWords = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
{
    {"un", 1}, {"uno", 1}, {"dos", 2}, {"tres", 3}, {"cuatro", 4}, {"cinco", 5},
    {"seis", 6}, {"siete", 7}, {"ocho", 8}, {"nueve", 9}, {"diez", 10},
    {"once", 11}, {"doce", 12}, {"trece", 13}, {"catorce", 14}, {"quince", 15},
    {"dieciseis", 16}, {"dieciséis", 16}, {"diecisiete", 17}, {"dieciocho", 18}, {"diecinueve", 19},
    {"veinte", 20}, {"veinti", 20},
    {"veintiun", 21}, {"veintiún", 21}, {"veintiuno", 21},
    {"veintidos", 22}, {"veintidós", 22},
    {"veintitres", 23}, {"veintitrés", 23},
    {"veinticuatro", 24}, {"veinticinco", 25},
    {"veintiseis", 26}, {"veintiséis", 26},
    {"veintisiete", 27}, {"veintiocho", 28}, {"veintinueve", 29},
    {"treinta", 30},
    {"treinta y un", 31}, {"treinta y uno", 31}
};

    private static int? ParseDayComponent(string dayText)
    {
        dayText = dayText.Trim().ToLowerInvariant();
        // Console.WriteLine($"DEBUG_ParseDayComponent: Input='{dayText}'");
        if (int.TryParse(dayText, out int dayNum))
        {
            if (dayNum >= 1 && dayNum <= 31) return dayNum;
        }

        if (dayText.Contains(" y "))
        {
            var parts = dayText.Split(new[] { " y " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                if (SpanishNumberWords.TryGetValue(parts[0], out int decade) &&
                    SpanishNumberWords.TryGetValue(parts[1], out int unit) &&
                    decade % 10 == 0 && unit >= 1 && unit <= 9)
                {
                    int potentialDay = decade + unit;
                    if (potentialDay >= 1 && potentialDay <= 31) return potentialDay;
                }
            }
        }
        return SpanishNumberWords.TryGetValue(dayText, out int wordDay) ? (wordDay >= 1 && wordDay <= 31 ? wordDay : (int?)null) : null;
    }

    private static int? ParseMonthComponent(string monthText, CultureInfo culture)
    {
        monthText = monthText.Trim().ToLowerInvariant();
        // Console.WriteLine($"DEBUG_ParseMonthComponent: Input='{monthText}'");
        for (int i = 1; i <= 12; i++)
        {
            if (culture.DateTimeFormat.GetMonthName(i).ToLowerInvariant() == monthText ||
                culture.DateTimeFormat.GetAbbreviatedMonthName(i).ToLowerInvariant() == monthText)
            {
                return i;
            }
        }
        return null;
    }

    private static int? ParseYearComponent(string yearText)
    {
        yearText = yearText.Trim();
        // Console.WriteLine($"DEBUG_ParseYearComponent: Input='{yearText}'");
        if (int.TryParse(yearText, out int yearNum))
        {
            if (yearNum > 1800 && yearNum < 2200) return yearNum;
        }

        yearText = yearText.ToLowerInvariant();
        if (yearText.StartsWith("dos mil "))
        {
            string remainderStr = yearText.Substring("dos mil ".Length).Trim();
            int? remainderNum = ParseDayComponent(remainderStr); // o una nueva ParseNumberWordUpTo99(remainderStr);
            if (remainderNum.HasValue && remainderNum.Value >= 0 && remainderNum.Value < 100)
            {
                return 2000 + remainderNum.Value;
            }
        }
        if (yearText == "dos mil quince") return 2015;
        if (yearText == "dos mil veintitres" || yearText == "dos mil veintitrés") return 2023;
        return null;
    }

    public static DateTime? TryParseDateInternal(string? dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString)) return null;

        string originalInputForLogging = dateString;
        //Console.WriteLine($"DEBUG_TryParseSpanishDateInternal: Input original='{originalInputForLogging}'");

        string normalizedDate = dateString.ToLowerInvariant().Trim();
        normalizedDate = Regex.Replace(normalizedDate, @"^[^\w\d\s]+|[.,]+$", "").Trim();
        // consideremos reemplazar múltiples espacios con uno solo si eso es un problema
        // normalizedDate = Regex.Replace(normalizedDate, @"\s+", " ").Trim();
        //Console.WriteLine($"DEBUG_TryParseSpanishDateInternal: Normalizado='{normalizedDate}'");

        var culture = new CultureInfo("es-ES"); // o es-AR

        string[] standardFormats = {
        "d 'de' MMMM 'de' yyyy", "dd 'de' MMMM 'de' yyyy",
        "d 'de' MMMM 'del año' yyyy", "dd 'de' MMMM 'del año' yyyy"
    };
        if (DateTime.TryParseExact(normalizedDate, standardFormats, culture, DateTimeStyles.None, out DateTime parsedDateExact))
        {
            //Console.WriteLine($"DEBUG: Parseado con TryParseExact: '{normalizedDate}' -> {parsedDateExact}");
            return parsedDateExact;
        }
        //Console.WriteLine($"DEBUG: TryParseExact falló para '{normalizedDate}' con formatos estándar.");

        // regex para formatos más descriptivos
        var detailedRegex = new Regex(
            @"^(?<day>[\w\s\u00C0-\u00FF]+?)\s+(?:días?\s+del\s+mes\s+de|de)\s+(?<month>[\w\u00C0-\u00FF]+)\s+(?:del\s+año\s+(?:de)?|de)\s+(?<year>[\w\s\u00C0-\u00FF\d]+)$",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        // `[\w\s\u00C0-\u00FF]` incluye letras acentuadas y espacios para partes como "dos mil quince" o "treinta y uno"
        // `.+?` para el día (no glotón), `\w+` para el mes (asume una palabra), `.+` para el año (glotón)

        var match = detailedRegex.Match(normalizedDate);

        if (match.Success)
        {
            //Console.WriteLine($"DEBUG: Regex detallado coincidió para '{normalizedDate}'");
            string dayPartStr = match.Groups["day"].Value.Trim();
            string monthPartStr = match.Groups["month"].Value.Trim();
            string yearPartStr = match.Groups["year"].Value.Trim();

            //Console.WriteLine($"DEBUG: Componentes Regex: Día='{dayPartStr}', Mes='{monthPartStr}', Año='{yearPartStr}'");

            int? day = ParseDayComponent(dayPartStr);
            int? month = ParseMonthComponent(monthPartStr, culture);
            int? year = ParseYearComponent(yearPartStr);

            //Console.WriteLine($"DEBUG: Componentes Parseados: Día={day}, Mes={month}, Año={year}");

            if (day.HasValue && month.HasValue && year.HasValue)
            {
                try
                {
                    var resultDate = new DateTime(year.Value, month.Value, day.Value);
                    //Console.WriteLine($"DEBUG: Fecha construida exitosamente: {resultDate} de '{originalInputForLogging}'");
                    return resultDate;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"AVISO: Componentes de fecha parseados forman una fecha inválida: D={day}, M={month}, Y={year} de '{originalInputForLogging}'");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"AVISO: Falló el parseo de uno o más componentes (D={day}, M={month}, Y={year}) usando el regex detallado para '{originalInputForLogging}'");
            }
        }
        else
        {
            //Console.WriteLine($"DEBUG: Regex detallado NO coincidió para '{normalizedDate}'");
        }

        // consideremos un último intento con DateTime.TryParse si los anteriores fallan,
        // aunque puede ser menos predecible para formatos no estándar
        // if (DateTime.TryParse(normalizedDate, culture, DateTimeStyles.AllowWhiteSpaces, out DateTime fallbackDate)) {
        //     Console.WriteLine($"DEBUG: Parseado con TryParse (fallback): '{normalizedDate}' -> {fallbackDate}");
        //     return fallbackDate;
        // }

        Console.WriteLine($"ADVERTENCIA FINAL: Falló el parseo completo de la cadena de fecha: '{originalInputForLogging}'");
        return null;
    }
}
