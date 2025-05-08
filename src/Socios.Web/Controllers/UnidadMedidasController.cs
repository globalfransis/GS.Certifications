using GS.Certifications.Application.UseCases.UnidadMedidas.Dto;
using GS.Certifications.Application.UseCases.UnidadMedidas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class UnidadMedidasController : Controller
{
    private readonly IMediator _mediator;

    public UnidadMedidasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<UnidadMedidaDto>>> GetAllAsync([FromQuery] GetUnidadMedidasQuery query)
    {
        return await _mediator.Send(query);
    }

}
