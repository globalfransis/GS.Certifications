using GS.Certifications.Web.Common.Resources;
using GS.Certifications.Web.Pages;
using Microsoft.Extensions.Localization;

namespace GS.Certifications.Web.Areas.Security.Pages.Usuarios.Actividades
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IStringLocalizer<Shared> loc)
        {
            Title = loc["Actividad Usuarios"];
        }
        public void OnGet()
        {
            GetStringTranslations();
        }
    }
}
