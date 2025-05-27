using GS.Certifications.Web.Common.Resources;
using GS.Certifications.Web.Pages;
using Microsoft.Extensions.Localization;

namespace GS.Certifications.Web.Areas.Socios.Pages.Certificaciones;

public class IndexModel : BasePageModel
{

    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Certificaciones"];
    }

    public void OnGet()
    {
        GetStringTranslations();
    }
}
