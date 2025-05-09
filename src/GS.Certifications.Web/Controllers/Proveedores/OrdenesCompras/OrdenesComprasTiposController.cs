using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Commands.TipoOrdenCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Queries;
using GS.Certifications.Application.UseCases.OrdenesCompras.Queries.TipoOrdenCompra;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.OrdenesCompras
{
    [Route("api/proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class OrdenesComprasTiposController : Controller
    {
        private readonly IMediator _mediator;

        public OrdenesComprasTiposController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompraTipoDto>> GetOneAsync([FromRoute] int id)
        {
            GetTipoOrdenCompraQuery query = new() { Id = id };
            var ordenCompra = await _mediator.Send(query);

            if (ordenCompra == null)
            {
                return NotFound();
            }

            return Ok(ordenCompra);
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<OrdenCompraTipoForListDto>>>
            GetAllAsync([FromQuery] GetTiposOrdenesComprasQuery query)
        {
            var ordenesCompras = await _mediator.Send(query);
            return Ok(ordenesCompras);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateAsync([FromBody] UpdateTipoOrdenCompraCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody] CreateTipoOrdenCompraCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            DeleteTipoOrdenCompraCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("ListaTiposOrdenesCompras")]
        public async Task<List<OrdenCompraTipoDto>> GetAllAsync([FromQuery] GetListaOrdenesComprasTiposQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
