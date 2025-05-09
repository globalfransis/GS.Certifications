using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace GS.Certifications.Web.Pages;

[AllowAnonymous]
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger, IStringLocalizer stringLocalizer)
    {
        var s = stringLocalizer["Test"];
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
