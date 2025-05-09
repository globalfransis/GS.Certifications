using GSFSharedResources;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;

namespace GS.Certifications.Web.Areas.Configuration.Pages.Periodos
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IStringLocalizer<Shared> loc)
        {
            Title = loc["Periodos"];
        }
        public void OnGet()
        {
        }
    }
}
