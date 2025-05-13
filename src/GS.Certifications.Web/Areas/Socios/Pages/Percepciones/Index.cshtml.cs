using GSFSharedResources;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;

namespace GS.Certifications.Web.Areas.Proveedores.Pages.Percepciones
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IStringLocalizer<Shared> loc)
        {
            Title = loc["Percepciones"];
        }
        public void OnGet()
        {
        }
    }
}
