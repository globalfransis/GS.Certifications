using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Users.Commands.PasswordChange;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using System.Threading.Tasks;
using GS.Certifications.Web.Common.Resources;

namespace GS.Certifications.Web.Areas.Security.Pages;

public class PasswordChangeModel : BasePageModel
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IStringLocalizer<Shared> _securityLoc;

    [BindProperty(SupportsGet = true)] public string Email { get; set; }

    [BindProperty(SupportsGet = true)] public string CurrentPassword { get; set; }

    [BindProperty(SupportsGet = true)] public string Password { get; set; }

    [BindProperty] public string PasswordConfirmation { get; set; }

    public PasswordChangeModel(ICurrentUserService currentUserService, IStringLocalizer<Shared> securityLoc)
    {
        _currentUserService = currentUserService;
        _securityLoc = securityLoc;
        Title = securityLoc["Cambio de Contrase�a"];
    }

    public async Task<IActionResult> OnPost()
    {
        var command = new PasswordChangeCommand(_currentUserService.UserId, CurrentPassword, Password, PasswordConfirmation);

        try
        {
            await Mediator.Send(command);
            SuccessMessage = _securityLoc["Se ha cambiado la contrase�a con �xito."];
            return RedirectToPage("/Login", new { area = "Security" });

        }
        catch (InvalidPasswordException)
        {
            ErrorMessage = _securityLoc["La contrase�a actual ingresada es incorrecta."];
            return Page();
        }

    }
    public void OnGet()
    {
        GetStringTranslations();
    }
}