using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Groups.Commands.GroupChangeGroupOwner;
using GSF.Application.Security.Groups.Queries.GetGroupCrud;
using GSF.Application.Segurity.Companies.Queries.GetGroupsByCurrentGroupOwner;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Areas.Security.Pages.Groups.Modals;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Security;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class GroupsController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected ICurrentUserService currentUserService;

    [HttpPost("ChangeOwner")]
    public async Task<IActionResult> ChangeOwnerAsync([FromBody] GroupDelegationPartialModel model,
                                                      [FromServices] IStringLocalizer<Shared> loc)
    {
        IActionResult result;
        try
        {
            var query = new GroupChangeGroupOwnerCommand()
            {
                GroupId = model.Id,
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

    [HttpGet("ListadoGrupos")]
    public async Task<List<GroupListDto>> GetGroupCrudDtos([FromQuery] GetGroupsByCurrentGroupOwnerQuery query)
    {
        return await Mediator.Send(query);
    }
}
