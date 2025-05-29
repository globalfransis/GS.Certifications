
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using GS.Certifications.Web.Common.Resources;

namespace GS.Certifications.Web.Areas.Interfaces.Pages.ReglasDefiniciones;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Definiciones de Reglas de Interfaces"];
    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}