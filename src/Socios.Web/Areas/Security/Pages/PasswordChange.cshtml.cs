using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Users.Commands.PasswordChange;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Security.Pages;

public class PasswordChangeModel : BasePageModel
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IStringLocalizer<SecurityResources> _securityLoc;

    [BindProperty(SupportsGet = true)] public string Email { get; set; }

    [BindProperty(SupportsGet = true)] public string CurrentPassword { get; set; }

    [BindProperty(SupportsGet = true)] public string Password { get; set; }

    [BindProperty] public string PasswordConfirmation { get; set; }

    public PasswordChangeModel(ICurrentUserService currentUserService, IStringLocalizer<SecurityResources> securityLoc)
    {
        _currentUserService = currentUserService;
        _securityLoc = securityLoc;
        Title = securityLoc["Cambio de Contraseña"];
    }

    public async Task<IActionResult> OnPost()
    {
        var command = new PasswordChangeCommand(_currentUserService.UserId, CurrentPassword, Password, PasswordConfirmation);

        try
        {
            await Mediator.Send(command);
            SuccessMessage = _securityLoc["Se ha cambiado la contraseña con éxito."];
            return RedirectToPage("/Login", new { area = "Security" });

        }
        catch (InvalidPasswordException)
        {
            ErrorMessage = _securityLoc["La contraseña actual ingresada es incorrecta."];
            return Page();
        }

    }
}