using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Commands;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Queries;
using GS.Certifications.Application.UseCases.OrdenesCompras.Commands;
using GS.Certifications.Application.UseCases.OrdenesCompras.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.ConceptosGastosTipos
{
    [Route("api/Configuration/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class ConceptosGastosTiposController : Controller
    {
        private readonly IMediator _mediator;

        public ConceptosGastosTiposController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptoGastoTipoDto>> GetOneAsync([FromRoute] int id)
        {
            GetConceptoGastoTipoQuery query = new() { Id = id };
            var ordenCompra = await _mediator.Send(query);

            if (ordenCompra == null)
            {
                return NotFound();
            }

            return Ok(ordenCompra);
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<ConceptoGastoTipoForListDto>>> 
            GetAllAsync([FromQuery] GetConceptosGastosTiposQuery query)
        {
            var conceptoGastoTipo = await _mediator.Send(query);
            return Ok(conceptoGastoTipo);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> 
            UpdateAsync([FromBody] UpdateConceptoGastoTipoCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> 
            PostAsync([FromBody] CreateConceptoGastoTipoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            DeleteConceptoGastoTipoCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("ListaConceptos")]
        public async Task<List<ConceptoGastoTipoDto>> GetAllAsync([FromQuery] GetListaConceptosGastosTiposQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
