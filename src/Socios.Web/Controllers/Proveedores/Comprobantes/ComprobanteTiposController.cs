using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Proveedores.Comprobantes;

[Route("api/Proveedores/[controller]")]
[ApiController]
[AuthorizationGSF]
public class ComprobanteTiposController : Controller
{
    private readonly IMediator _mediator;

    public ComprobanteTiposController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ComprobanteTipoDto>>> GetAllAsync([FromQuery] GetComprobanteTiposQuery query)
    {
        return await _mediator.Send(query);
    }

}
