using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class CondicionVentasController : Controller
{
    private readonly IMediator _mediator;

    public CondicionVentasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CondicionVentaDto>>> GetAllAsync([FromQuery] GetCondicionVentasQuery query)
    {
        return await _mediator.Send(query);
    }

}
