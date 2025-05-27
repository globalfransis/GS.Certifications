using GSFSharedResources;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Socios.Pages.Certificaciones;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Solicitudes"];
        ShowTitle = false;
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}
