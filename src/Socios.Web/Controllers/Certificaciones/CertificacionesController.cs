using GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Queries;
using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Common.Services.Empresa;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Certificaciones
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class CertificacionesController : Controller
    {
        private readonly IMediator _mediator;
        private ICurrentSocioService _currentSocioService;

        public CertificacionesController(IMediator mediator, ICurrentSocioService currentSocioService)
        {
            _mediator = mediator;
            _currentSocioService = currentSocioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetOneAsync([FromRoute] int id)
        {
            // Replace 'object' with the specific response type
            throw new NotImplementedException();
        }

        [HttpGet("{id}/Solicitudes")]
        public async Task<ActionResult<IPaginatedQueryResult<object>>> GetAllAsync([FromQuery] GetSolicitudCertificacionesQuery query, [FromRoute] int id)
        {
            var currentSocioId = _currentSocioService.GetCurrentEmpresaPortalId();
            query.SocioId = currentSocioId;
            
            query.CertificacionId = id;

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("Solicitudes/Estados")]
        public async Task<ActionResult<List<SolicitudCertificacionEstadoDto>>> GetEstadosAsync([FromQuery] GetSolicitudCertificacionEstadosQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateAsync([FromRoute] int id)
        {
            // Replace 'Unit' with the specific result type for the command
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAsync([FromBody] IRequest command)
        {
            // Replace 'IRequest' with the specific command type and 'int' with the return value
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            // Replace 'Unit' with the specific result type for the command
            throw new NotImplementedException();
        }
    }
}
