using GSF.Application.Security.Users.Commands.UserActivation;
using GSF.Application.Security.Users.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Pages;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages;

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
            ModelState.AddModelError("", "El usuario no existe.");
        }
        catch (InvalidUserActivationTokenException)
        {
            ModelState.AddModelError("", "El pedido de activación de cuenta es inválido o está vencido. Comuniquesé con un administrador.");
        }
    }
}