using Microsoft.AspNetCore.Authorization;

namespace GS.Certifications.Web.Pages;

[Authorize]
public class IndexModel : BasePageModel
{
    public IndexModel()
    {
        Title = "Inicio";
        //Title = "Home";
        ShowTitle = false;

    }
    public string HtmlChangeLog { get; set; }

    public void OnGet()
    {
        GetStringTranslations();
    }

    private void LoadChangelog()
    {

    }
}
