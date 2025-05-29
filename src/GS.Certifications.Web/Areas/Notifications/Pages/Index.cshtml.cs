using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using GS.Certifications.Web.Common.Resources;

namespace GS.Certifications.Web.Areas.Notifications.Pages;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Gestión de Notificaciones"];
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}
