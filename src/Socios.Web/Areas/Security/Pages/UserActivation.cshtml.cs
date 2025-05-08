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
            ViewData["UserActivationMessage"] = $"<div class='alert alert-success text-center'>Su cuenta de </br><span class='fw-bold'>rettorijulian@gmail.com</span></br>ha sido activada con <strong>¡Éxito!</strong>.</div>";
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