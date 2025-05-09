using GSF.Application.Security.Users.Commands.UserChangeGroupOwner;
using GSF.Application.Security.Users.Queries;
using GSF.Application.Security.Users.Queries.GetUsersByCurrentGroupOwner;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Areas.Security.Pages.Users.Modals;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Security;


[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]

public class UserController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpPost]
    public async Task OnPostLogOutAsync()
    {
        await HttpContext.SignOutAsync();

        RedirectToAction("Login", "Security");
    }

    [HttpPost("ChangeOwner")]
    public async Task<IActionResult> ChangeOwnerAsync([FromBody] UserDelegationPartialModel model,
                                                      [FromServices] IStringLocalizer<Shared> loc)
    {
        IActionResult result;
        try
        {
            var query = new UserChangeGroupOwnerCommand()
            {
                UserId = model.Id,
                Login = model.Login,
                //Modified = DateTime.Parse(model.Modified),
                SelectedGroupOwnerId = model.SelectedGroupOwnerId,
                RowVersion = model.RowVersion
            };
            _ = await Mediator.Send(query);
            result = new StatusCodeResult((int)HttpStatusCode.OK);
        }
        catch (DbUpdateConcurrencyException)
        {
            result = BadRequest(new { Message = loc["Los datos fueron modificados por otro usuario. Intente nuevamente."] });
        }

        return result;
    }
    [HttpGet("GetUsers")]
    public async Task<List<UserListDto>> GetUsers()
    {
        var query = new GetUsersByCurrentGroupOwnerQuery();

        var users = await Mediator.Send(query);

        return users;
    }



}
