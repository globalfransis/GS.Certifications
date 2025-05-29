
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using GS.Certifications.Web.Common.Resources;

namespace GS.Certifications.Web.Areas.Configuracion.Pages.Parametros;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Parámetros"];
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}
