using GSF.Application.Parameters.IdentificationTypes.Queries.GetIdentificationTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Security;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class IdentificationTypeController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet("GetIdentificationTypesByOrganization")]
    public async Task<List<IdentificationTypeDto>> GetIdentificationTypesByOrganization()
    {
        var query = new GetIdentificationTypesQuery();
        List<IdentificationTypeDto> identificationTypes = await Mediator.Send(query);

        return identificationTypes;
    }
}