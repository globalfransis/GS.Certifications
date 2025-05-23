using GSF.Application.Security.Users.Commands.UserActivation;
using GSF.Application.Security.Users.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Pages;

namespace Socios.Web.Areas.Security.Pages;

[AllowAnonymous]
public class UserActivation : BasePageModel
{
    [BindProperty(SupportsGet = true)] public string Token { get; set; }

    public async Task OnGet()
    {
        try
        {
            var email = await Mediator.Send(new ValidateUserActivationTokenCommand() { Token = Token });
            ViewData["UserActivationMessage"] = $"Se ha activado su cuenta de {email} con éxito.";
        }
        catch (NonExistentUserException)
        {
            ModelState.AddModelError("", "No se ha podido realizar el pedido de activación de su cuenta. Comuníquese con un administrador.");
        }
        catch (InvalidUserActivationTokenException)
        {
            ModelState.AddModelError("", "No se ha podido realizar el pedido de activación de su cuenta. Comuníquese con un administrador.");
        }
    }
}