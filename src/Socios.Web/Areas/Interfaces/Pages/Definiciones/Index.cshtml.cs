using GSFSharedResources;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Interfaces.Pages.Definiciones;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Definiciones de Interfaces"];
    }
}