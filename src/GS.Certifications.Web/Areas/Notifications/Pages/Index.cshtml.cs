using GSFSharedResources;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;

namespace GS.Certifications.Web.Areas.Notifications.Pages;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Gesti�n de Notificaciones"];
    }
    public void OnGet()
    {
    }
}
