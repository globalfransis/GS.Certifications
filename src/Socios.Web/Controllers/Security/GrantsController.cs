using GSF.Application.Common.Interfaces;
using GSF.Application.Security.SecurityTemp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Security;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class GrantsController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected ICurrentUserService currentUserService;

    [HttpGet("GrantsExists")]
    public async Task<Dictionary<string, bool>> GetGrantsExistance([FromQuery] string[] grants,
                                                                           //[FromServices] IStringLocalizer<Shared> loc,
                                                                           [FromServices] ISecurityTempStoreService securityTempStoreService)
    {
        Dictionary<string, bool> result = new Dictionary<string, bool>();
        //bool result = (await securityTempStoreService.GetGrantAsync(grantName)) != null;
        foreach (var grantName in grants)
        {
            result.Add(grantName, await securityTempStoreService.GetGrantAsync(grantName) != null);
        }

        return result;
    }
}
