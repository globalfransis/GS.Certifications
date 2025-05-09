using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Alicuotas;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Alicuotas
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class AlicuotasController : Controller
    {
        private readonly IMediator _mediator;

        public AlicuotasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<AlicuotaDto>>
            GetAllAlicuotas([FromQuery] GetAlicuotasQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("ObtenerAlicuotas")]
        public async Task<ActionResult<IPaginatedQueryResult<AlicuotaDto>>>
            GetAllAsync([FromQuery] GetManyAlicuotasQuery query)
        {
            var alicuotas = await _mediator.Send(query);
            return Ok(alicuotas);
        }
    }
}
