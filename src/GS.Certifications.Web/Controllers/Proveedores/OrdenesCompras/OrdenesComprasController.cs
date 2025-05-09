using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Commands;
using GS.Certifications.Application.UseCases.OrdenesCompras.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.OrdenesCompras
{
    [Route("api/Proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class OrdenesComprasController : Controller
    {
        private readonly IMediator _mediator;

        public OrdenesComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompraDto>> GetOneAsync([FromRoute] int id)
        {
            GetOrdenCompraQuery query = new() { Id = id };
            var ordenCompra = await _mediator.Send(query);

            if (ordenCompra == null)
            {
                return NotFound();
            }

            return Ok(ordenCompra);
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<OrdenCompraForListDto>>>
            GetAllAsync([FromQuery] GetOrdenesComprasQuery query)
        {
            var ordenesCompras = await _mediator.Send(query);
            return Ok(ordenesCompras);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateAsync([FromBody] UpdateOrdenCompraCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody] CreateOrdenCompraCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            DeleteOrdenCompraCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
