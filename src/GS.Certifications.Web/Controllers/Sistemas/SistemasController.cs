using GSF.Application.Sistemas.Dto;
using GSF.Application.Sistemas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Sistemas;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class SistemasController : Controller
{
    private IMediator _mediator;
    public SistemasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SistemaDto>>> GetSistemasAsync([FromQuery] GetSistemasQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}
