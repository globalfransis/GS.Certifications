using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Parametros.Commands.CreateParametro;
using GSF.Application.Parametros.Commands.UpdateParametro;
using GSF.Application.Parametros.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Net;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Configuracion.Parametros;

[ApiController]
[AuthorizationGSF]
[Route("api/[controller]")]
public class ParametrosController : Controller
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public ParametrosController(IMediator mediator) => _mediator = mediator;

    [HttpGet("GetParametros")]
    public async Task<IPaginatedQueryResult<ParametroDto>> GetParametros([FromQuery] GetParametrosQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("GetParametro")]
    public async Task<ParametroDto> GetAsync([FromQuery] GetParametroQuery query) => await _mediator.Send(query);

    [HttpPost("CreateParametro")]
    public async Task<long> PostParametro([FromBody] CreateParametroCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return result;
    }

    [HttpPut("UpdateParametro")]
    public async Task<IActionResult> PutParametro([FromBody] UpdateParametroCommand command)
    {
        var result = await _mediator.Send(command);

        return new StatusCodeResult((int)HttpStatusCode.OK);
    }
}
