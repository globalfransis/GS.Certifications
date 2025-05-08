using GSFSharedResources;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Notifications.Pages;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Gestión de Notificaciones"];
    }
    public void OnGet()
    {
    }
}
