using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.UseCases.UnidadMedidas.Dto;
using GS.Certifications.Application.UseCases.UnidadMedidas.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers;

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
