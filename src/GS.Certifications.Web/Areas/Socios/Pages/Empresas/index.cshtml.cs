
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using GS.Certifications.Web.Common.Resources;

namespace GS.Certifications.Web.Areas.Proveedores.Pages.Administracion;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Administración"];
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}