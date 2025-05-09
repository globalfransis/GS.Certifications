using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Roles.Commands.RoleChangeGroupOwner;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Areas.Security.Pages.Roles.Modals;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Net;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Security;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class RolesController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected ICurrentUserService currentUserService;

    //[HttpGet]
    //public async Task<List<OptionGrantTreeDto>> Get()
    //{
    //    var optionsQuery = new GetTreeQuery();
    //    List<OptionGrantTreeDto> Options = await Mediator.Send(optionsQuery);

    //    return Options;
    //}

    //private OptionGrantTreeDto findOptionById(List<OptionGrantTreeDto> Options, long id)
    //{
    //    OptionGrantTreeDto result = null;
    //    int i = 0;

    //    while(i < Options.Count && result == null)
    //    {
    //        if(Options[i].Id == id)
    //        {
    //            result = Options[i];
    //        }
    //        else
    //        {
    //            result = findOptionById(Options[i].Options, id);
    //        }
    //        i++;
    //    }

    //    return result;
    //}

    //[HttpPost]
    //public async Task Post([FromBody] CreateRoleCommand command)
    //{
    //    _ = await Mediator.Send(command);
    //}

    [HttpPost("ChangeOwner")]
    public async Task<IActionResult> ChangeOwnerAsync([FromBody] RoleDelegationPartialModel model,
                                                      [FromServices] IStringLocalizer<Shared> loc)
    {
        IActionResult result;
        try
        {
            var query = new RoleChangeGroupOwnerCommand()
            {
                RoleId = model.Id,
                Name = model.Name,
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
}
