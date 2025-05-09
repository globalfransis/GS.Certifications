using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.UseCases.Impuestos.Commands;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Impuestos
{
    [Route("api/Proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class ImpuestosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentCompanyService currentCompanyService;

        public ImpuestosController(IMediator mediator, ICurrentCompanyService currentCompanyService)
        {
            _mediator = mediator;
            this.currentCompanyService = currentCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImpuestoDto>>> 
            GetAllAsync([FromQuery] GetImpuestosQuery query)
        {
            var currentCompanyId = await currentCompanyService.GetCurrentCompanyIdLazyAsync();
            query.CompanyId = currentCompanyId;
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImpuestoBackendDto>> 
            GetOneAsync([FromRoute] int id)
        {
            GetImpuestoQuery query = new() { Id = id };
            var empresa = await _mediator.Send(query);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [HttpGet("ListaImpuestos")]
        public async Task<ActionResult<IPaginatedQueryResult<ImpuestoForListDto>>>
            GetAllImpuestosAsync([FromQuery] GetManyImpuestosQuery query)
        {
            var empresas = await _mediator.Send(query);
            return Ok(empresas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> 
            UpdateAsync([FromBody] UpdateImpuestoCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<long>> 
            PostAsync([FromBody] CreateImpuestoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> 
            DeleteAsync([FromRoute] int id)
        {
            DeleteImpuestoCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
