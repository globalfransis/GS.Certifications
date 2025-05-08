namespace Socios.Web.Common.Services;

public static class FormatService
{
    public const string MONEY_FORMAT = "#,0.00";
    public const string DATE_FORMAT = "dd/MM/yyyy";

    public static string ToStringUI(this DateTime date)
    {
        return date.ToString(DATE_FORMAT);
    }
}
