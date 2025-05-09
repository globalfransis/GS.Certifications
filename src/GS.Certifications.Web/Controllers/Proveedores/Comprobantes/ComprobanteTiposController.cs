using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Comprobantes;

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
