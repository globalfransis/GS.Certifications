using GSF.Application.Security.Users.Commands.PasswordChange;
using GSF.Application.Security.Users.Commands.PasswordRecovery;
using GSF.Application.Security.Users.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Security.Pages;

[AllowAnonymous]
public class PasswordRecoveryModel : BasePageModel
{
    [BindProperty(SupportsGet = true)] public string Email { get; set; }

    [BindProperty(SupportsGet = true)] public string Token { get; set; }

    [BindProperty(SupportsGet = true)] public string Password { get; set; }

    [BindProperty] public string PasswordConfirmation { get; set; }

    public bool PasswordChanged { get; private set; } = false;

    public async Task OnGet()
    {
        try
        {
            Email = await Mediator.Send(new ValidatePasswordRecoveryTokenCommand() { Token = Token });

        }
        catch (NonExistentUserException)
        {
            ModelState.AddModelError("", "El usuario no existe.");
        }
        catch (InvalidPasswordChangeTokenException)
        {
            ModelState.AddModelError("", "El pedido de cambio de contraseña es inválido o está vencido. Solicítelo nuevamente.");
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var command = new PasswordRecoveryChangeCommand(Token, Password, PasswordConfirmation);

        try
        {
            await Mediator.Send(command);

            SuccessMessage = $"Se ha cambiado la contraseña con éxito.";

            PasswordChanged = true;

            return RedirectToPage("/Login", new { area = "Security" });
        }
        catch (NonExistentUserException)
        {
            ModelState.AddModelError("", "El usuario no existe.");
        }
        catch (InvalidPasswordChangeTokenException)
        {
            ModelState.AddModelError("", "El pedido de cambio de contraseña es inválido o está vencido. Solicítelo nuevamente.");
        }

        return Page();

    }
}