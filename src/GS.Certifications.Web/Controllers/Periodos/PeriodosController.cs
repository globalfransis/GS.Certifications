using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Periodos;
using GS.Certifications.Application.UseCases.Periodos.Commands;
using GS.Certifications.Application.UseCases.Periodos.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Periodos
{
    [Route("api/Configuration/[controller]")]
    [ApiController]
    //[AuthorizationGSF]
    public class PeriodosController : Controller
    {
        private readonly IMediator _mediator;

        public PeriodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodoDto>> GetOneAsync([FromRoute] int id)
        {
            GetPeriodoQuery query = new()
            {
                Id = id
            };

            var periodo = await _mediator.Send(query);

            if (periodo == null)
            {
                return NotFound();
            }

            return Ok(periodo);
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<PeriodoForListDto>>> GetAllAsync([FromQuery] GetPeriodosQuery query)
        {
            var periodos = await _mediator.Send(query);
            return Ok(periodos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateAsync([FromBody] UpdatePeriodoCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody] CreatePeriodoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            DeletePeriodoCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("EstadosPeriodos")]
        public async Task<List<EstadoPeriodoDto>> GetAllEstadosPeriodosAsync([FromQuery] GetEstadosPeriodosQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
