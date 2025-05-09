using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Middleware.Interfaces.Dto;
using GSF.Application.Middleware.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Interfaces;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class InterfacesController : Controller
{
    private IMediator _mediator;
    public InterfacesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ResultadosProcesos")]
    public async Task<ActionResult<IPaginatedQueryResult<InterfazProcesoDto>>> GetResultadosProcesosAsync([FromQuery] GetResultadosProcesosQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("Campos")]
    public async Task<ActionResult<IPaginatedQueryResult<InterfazCampoDto>>> GetCamposAsync([FromQuery] GetInterfazCamposQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("Campos/{id}")]
    public async Task<ActionResult<InterfazCampoDto>> GetInterfazCampoAsync([FromRoute] int id)
    {
        var query = new GetInterfazCampoQuery() { Id = id };
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("Reglas")]
    public async Task<ActionResult<IPaginatedQueryResult<InterfazReglaDto>>> GetReglasAsync([FromQuery] GetInterfazReglasQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("Reglas/{id}")]
    public async Task<ActionResult<InterfazReglaDto>> GetInterfazReglaAsync([FromRoute] int id)
    {
        var query = new GetInterfazReglaQuery() { Id = id };
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("ResultadosProcesos/{id}")]
    public async Task<ActionResult<InterfazProcesoDto>> GetResultadoProcesoAsync([FromRoute] int id)
    {
        var query = new GetResultadoProcesoQuery() { Id = id };
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("ResultadosProcesos/{id}/Archivos")]
    public async Task<FileResult> DescargarResultadoProcesoArchivoAsync([FromRoute] int id)
    {
        var query = new GetResultadoProcesoArchivoQuery() { Id = id };
        var result = await _mediator.Send(query);

        FileInfo fileInfo = new(result.FileName);
        new FileExtensionContentTypeProvider().Mappings.TryGetValue(fileInfo.Extension, out var contenttype);

        return File(result.Data, contenttype, fileInfo.Name);
    }

    [HttpGet("{id}/Estados")]
    public async Task<ActionResult<IEnumerable<InterfazEstadoDto>>> GetInterfazEstadosAsync([FromRoute] short id, [FromQuery] GetInterfazEstadosQuery query)
    {
        query.InterfazId = id;
        return Ok(await _mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterfazDto>>> GetInterfacesAsync([FromQuery] GetInterfacesQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

}