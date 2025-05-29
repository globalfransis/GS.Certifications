
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using GS.Certifications.Web.Common.Resources;

namespace GS.Certifications.Web.Areas.Interfaces.Pages.ResultadosProcesos;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Resultados de Procesos"];
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}