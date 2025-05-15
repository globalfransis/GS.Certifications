using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;
using GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Commands;
using GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Queries;
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

        [HttpGet("Solicitudes/{solicitudId}")]
        public async Task<ActionResult<SolicitudCertificacionDto>> GetOneAsync([FromRoute] int solicitudId)
        {
            var query = new GetSolicitudCertificacionQuery() { Id = solicitudId };
            var result = await _mediator.Send(query);
            return Ok(result);
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

        [HttpPost("{id}/Solicitudes")]
        public async Task<ActionResult<int>> PostAsync([FromBody] CreateSolicitudCertificacionCommand command, [FromRoute] int id)
        {
            var currentSocioId = _currentSocioService.GetCurrentEmpresaPortalId();

            command.SocioId = (int)currentSocioId;
            command.CertificacionId = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            // Replace 'Unit' with the specific result type for the command
            throw new NotImplementedException();
        }
    }
}
