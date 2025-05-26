using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Socios.Pages.RequisitosFQS;
public class IndexModel : BasePageModel
{
    public IndexModel(IStringLocalizer<RequisitosFQS> loc)
    {
        Title = loc["Requisitos FQS"];
        ShowTitle = false;
    }
    public void OnGet()
    {
    }
}
