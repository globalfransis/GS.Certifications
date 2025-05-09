using AlertasGSF.Dtos;
using GSF.Application.Alertas;
using GSF.Application.Alertas.Commands;
using GSF.Application.Alertas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Alertas;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class AlertasController : Controller
{
    private IMediator _mediator;
    public AlertasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetDestinatarioVariables")]
    public async Task<List<DestinatarioVariableDto>> GetDestinatarioVariablesQuery([FromQuery] GetDestinatarioVariablesQuery query) => await _mediator.Send(query);

    [HttpGet("GetAlertaTipos")]
    public async Task<List<AlertaTipoDto>> GetAlertaTiposQuery([FromQuery] GetAlertaTipoQuery query) => await _mediator.Send(query);

    [HttpGet("GetAlertaTipoEstados")]
    public async Task<List<AlertaTipoEstadoDto>> GetAlertaTiposQuery([FromQuery] GetAlertaTipoEstadosQuery query) => await _mediator.Send(query);

    [HttpGet("GetAlertaTipoUbicaciones")]
    public async Task<List<AlertaTipoUbicacionDto>> GetAlertaUbicacionesQuery([FromQuery] GetAlertaTipoUbicacionesQuery query) => await _mediator.Send(query);

    [HttpGet("GetReglasAdicionalesByAlertaTipo")]
    public async Task<List<ReglaAdicionalDto>> GetReglasAdicionalesByAlertaTipoQuery([FromQuery] GetReglasAdicionalesByAlertaTipoQuery query) => await _mediator.Send(query);

    [HttpGet("GetCamposVariablesByAlertaTipo")]
    public async Task<List<CampoVariableDto>> GetCamposVariablesByAlertaTipoQuery([FromQuery] GetCamposVariablesByAlertaTipoQuery query) => await _mediator.Send(query);

    [HttpGet("GetAlertas")]
    public async Task<AlertasSearchResponse> GetAlertasQuery([FromQuery] GetAlertasQuery query) => await _mediator.Send(query);

    [HttpGet("GetAlerta")]
    public async Task<AlertaDto> GetAlertaQuery([FromQuery] GetAlertaQuery query) => await _mediator.Send(query);

    [HttpPost("CreateAlerta")]
    public async Task CreateAlerta([FromBody] CreateAlertaCommand command) => await _mediator.Send(command);

    [HttpPut("UpdateAlerta")]
    public async Task UpdateAlerta([FromBody] UpdateAlertaCommand command) => await _mediator.Send(command);
}
