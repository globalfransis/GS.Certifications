using GSFSharedResources;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;

namespace GS.Certifications.Web.Areas.Configuration.Pages.TiposOrdenesCompras;

public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<Shared> loc)
    {
        Title = loc["Tipos de Documentos de Compra"];
    }
    public void OnGet()
    {
    }
}
