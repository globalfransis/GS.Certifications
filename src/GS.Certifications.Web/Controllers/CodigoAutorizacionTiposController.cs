using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Dto;
using GS.Certifications.Application.UseCases.CodigoAutorizacionTipos.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers;

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
