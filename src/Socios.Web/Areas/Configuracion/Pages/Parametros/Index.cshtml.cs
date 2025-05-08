using GSFSharedResources;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Configuracion.Pages.Parametros;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Parámetros"];
    }
    public void OnGet()
    {
    }
}
