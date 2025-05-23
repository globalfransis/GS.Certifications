using GSFSharedResources;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;

namespace GS.Certifications.Web.Areas.Socios.Pages.Certificaciones;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Certificaciones"];
    }
    public void OnGet()
    {
    }
}
