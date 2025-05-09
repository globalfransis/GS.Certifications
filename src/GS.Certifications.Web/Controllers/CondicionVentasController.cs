using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers;


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
