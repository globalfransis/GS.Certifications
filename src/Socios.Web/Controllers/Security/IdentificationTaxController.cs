using GSF.Application.Parametrics.IdentificationTaxes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Security;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class IdentificationTaxController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet("GetIdentificationTaxesByOrganization")]
    public async Task<List<IdentificationTaxDto>> GetIdentificationTaxesByOrganization()
    {
        var query = new GetIdentificationTaxesQuery();
        List<IdentificationTaxDto> identificationTaxes = await Mediator.Send(query);

        return identificationTaxes;
    }
}