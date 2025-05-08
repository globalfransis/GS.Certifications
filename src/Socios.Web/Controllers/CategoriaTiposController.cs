using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.CategoriaTipos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class CategoriaTiposController : Controller
{
    private readonly IMediator _mediator;

    public CategoriaTiposController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaTipoDto>>> GetAllAsync([FromQuery] GetCategoriaTiposQuery query)
    {
        return await _mediator.Send(query);
    }

}
