using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.Commons.Dtos.Percepciones;
using GS.Certifications.Application.UseCases.Impuestos.Commands;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using GS.Certifications.Application.UseCases.Percepciones.Commands;
using GS.Certifications.Application.UseCases.Percepciones.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Percepciones
{
    [Route("api/Proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class PercepcionesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentCompanyService currentCompanyService;

        public PercepcionesController(IMediator mediator, ICurrentCompanyService currentCompanyService)
        {
            _mediator = mediator;
            this.currentCompanyService = currentCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PercepcionDto>>> GetAllAsync([FromQuery] GetPercepcionesQuery query)
        {
            var currentCompanyId = await currentCompanyService.GetCurrentCompanyIdLazyAsync();
            query.CompanyId = currentCompanyId;
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PercepcionBackendDto>>
            GetOneAsync([FromRoute] int id)
        {
            GetPercepcionQuery query = new() { Id = id };
            var empresa = await _mediator.Send(query);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [HttpGet("ListaPercepciones")]
        public async Task<ActionResult<IPaginatedQueryResult<PercepcionForListDto>>>
            GetAllImpuestosAsync([FromQuery] GetManyPercepcionesQuery query)
        {
            var empresas = await _mediator.Send(query);
            return Ok(empresas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>>
            UpdateAsync([FromBody] UpdatePercepcionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<long>>
            PostAsync([FromBody] CreatePercepcionCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>>
            DeleteAsync([FromRoute] int id)
        {
            DeletePercepcionCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
