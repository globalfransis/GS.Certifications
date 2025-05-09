using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Notifications.Events.Commands;
using GSF.Application.Notifications.Events.Dto;
using GSF.Application.Notifications.Events.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Notifications;

[Route("api/Notifications/[controller]")]
[ApiController]
[AllowAnonymous]
public class EventsController : Controller
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateNotificacionEvento([FromBody] CreateEventoCommand command)
    {
        var request = HttpContext.Request;

        //request.Headers.TryGetValue("secret-key", out StringValues secretKey);

        //if (secretKey != "fc2ffb3b3cb2e62b944ec603764efa9a")
        //{
        //    return Unauthorized("Invalid API Key.");
        //}

        return Ok(await _mediator.Send(command));
    }

    [HttpGet]
    public async Task<IPaginatedQueryResult<EventoDto>> GetEventos([FromQuery] GetEventosQuery query) => await _mediator.Send(query);
}
