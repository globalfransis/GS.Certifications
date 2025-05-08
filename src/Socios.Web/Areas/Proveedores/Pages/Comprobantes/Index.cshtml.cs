using GSFSharedResources;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Proveedores.Pages.Comprobantes;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        //Title = loc["Comprobantes"];
    }
    public void OnGet()
    {
    }
}
