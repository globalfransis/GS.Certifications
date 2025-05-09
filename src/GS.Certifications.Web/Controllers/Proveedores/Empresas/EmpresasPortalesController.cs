using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;

namespace GS.Certifications.Web.Controllers.Proveedores.Empresas
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class EmpresasPortalesController : Controller
    {
        private readonly IMediator _mediator;

        public EmpresasPortalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaPortalDto>>> GetAllAsync([FromQuery] GetEmpresasWithoutPaginationQuery query)
        {
            var empresas = await _mediator.Send(query);
            return Ok(empresas);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Unit>> UpdateAsync([FromRoute] int id)
        //{
        //    // Replace 'Unit' with the specific result type for the command
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //public async Task<ActionResult<int>> PostAsync([FromBody] IRequest command)
        //{
        //    // Replace 'IRequest' with the specific command type and 'int' with the return value
        //    throw new NotImplementedException();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        //{
        //    // Replace 'Unit' with the specific result type for the command
        //    throw new NotImplementedException();
        //}
    }
}
