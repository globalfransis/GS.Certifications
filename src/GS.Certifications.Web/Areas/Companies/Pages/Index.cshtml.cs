using GSFSharedResources;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;

namespace GS.Certifications.Web.Areas.Companies.Pages;
public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Empresas"];
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}