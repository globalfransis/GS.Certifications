using GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Dto;
using GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class CodigoAutorizacionTiposController : Controller
{
    private readonly IMediator _mediator;

    public CodigoAutorizacionTiposController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CodigoAutorizacionTipoDto>>> GetAllAsync([FromQuery] GetCodigoAutorizacionTiposQuery query)
    {
        return await _mediator.Send(query);
    }

}
