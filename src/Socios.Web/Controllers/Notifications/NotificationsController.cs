using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Notifications;
using GSF.Application.Notifications.Commands;
using GSF.Application.Notifications.Events.Dto;
using GSF.Application.Notifications.Events.Queries;
using GSF.Application.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Notifications;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class NotificationsController : Controller
{
    private IMediator _mediator;
    public NotificationsController(IMediator mediator) => _mediator = mediator;


    [HttpGet("GetNotificacionEstados")]
    public async Task<List<NotificacionEstadoDto>> GetNotificacionEstadosQuery([FromQuery] GetNotificacionEstadosQuery query) => await _mediator.Send(query);

    [HttpGet("GetNotificaciones")]
    public async Task<IPaginatedQueryResult<NotificacionListDto>> GetNotificacionesQuery([FromQuery] GetNotificacionesQuery query) => await _mediator.Send(query);

    [HttpGet("GetNotificacion")]
    public async Task<NotificacionGestionDto> GetNotificacionQuery([FromQuery] GetNotificacionQuery query) => await _mediator.Send(query);

    [HttpGet("GetNotificationContent")]
    public async Task<NotificationContentDto> GetNotificationContent([FromQuery] GetNotificationContentQuery query) => await _mediator.Send(query);

    [HttpPost("CreateNotificacionReenvio")]
    public async Task<Unit> CreateNotificacionReenvio([FromBody] CreateNotificacionReenvioCommand command) => await _mediator.Send(command);

    [HttpGet("{notificacionId}/Events")]
    public async Task<ActionResult<IDictionary<string, List<EventoDto>>>> GetEventosByNotificacion([FromRoute] long notificacionId)
    {
        GetEventosByNotificacionQuery query = new()
        {
            Id = notificacionId
        };

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
