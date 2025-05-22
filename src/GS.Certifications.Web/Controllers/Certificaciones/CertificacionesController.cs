using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;
using GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Commands;
using GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Queries;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Queries;
using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Application.Common.Interfaces;

namespace GS.Certifications.Web.Controllers.Certificaciones
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class CertificacionesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService currentUserService;

        public CertificacionesController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet("Solicitudes/{solicitudId}")]
        public async Task<ActionResult<SolicitudCertificacionDto>> GetOneAsync([FromRoute] int solicitudId)
        {
            var query = new GetSolicitudCertificacionQuery() { Id = solicitudId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("Solicitudes/Documentos/{documentoId}")]
        public async Task<ActionResult<SolicitudCertificacionDto>> GetDocumentoAsync([FromRoute] int documentoId)
        {
            var query = new GetSolicitudCertificacionDocumentoQuery() { Id = documentoId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}/Solicitudes")]
        public async Task<ActionResult<IPaginatedQueryResult<object>>> GetAllAsync([FromQuery] GetSolicitudCertificacionesQuery query, [FromRoute] int id)
        {
            // TODO: el id del socio es un parametro del querystring

            query.CertificacionId = id;

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Solicitudes/{solicitudId}/Documentos/{id}/Analisis")]
        public async Task<ActionResult<int>> AnalyzeAsync([FromRoute] int solicitudId, [FromRoute] int id)
        {
            if (HttpContext.Request.Form.Files.Count == 0) return BadRequest("Se debe enviar un archivo.");

            // TODO: el id del socio es un parametro del querystring
            //var currentSociolId = _currentSocioService.GetCurrentEmpresaPortalId();

            var cmd = new AnalyzeDocumentoSolicitudCertificacionCommand
            {
                Id = id,
                SolicitudId = solicitudId,
                //SocioId = (int)currentSociolId,
                FormFile = HttpContext.Request.Form.Files[0]
            };

            return Ok(await _mediator.Send(cmd));
        }

        [HttpGet("Solicitudes/Estados")]
        public async Task<ActionResult<List<SolicitudCertificacionEstadoDto>>> GetEstadosAsync([FromQuery] GetSolicitudCertificacionEstadosQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("Solicitudes/{id}")]
        public async Task<ActionResult<Unit>> UpdateSolicitudAsync([FromRoute] int id, [FromBody] AprobarSolicitudCertificacionCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }


        [HttpPut("Solicitudes/{id}/Rechazos")]
        public async Task<ActionResult<Unit>> RechazoSolicitudAsync([FromRoute] int id, [FromBody] RechazarSolicitudCertificacionCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("Solicitudes/Documentos/{id}/Validaciones")]
        public async Task<ActionResult<Unit>> ValidarDocumentoSolicitudAsync([FromRoute] int id, [FromBody] ValidarDocumentoSolicitudCertificacionCommand command)
        {
            command.Id = id;
            command.ValidadoPorId = currentUserService.UserId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("Solicitudes/Documentos/{id}/Rechazos")]
        public async Task<ActionResult<Unit>> RechazarDocumentoSolicitudAsync([FromRoute] int id, [FromBody] RechazarDocumentoSolicitudCertificacionCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("Solicitudes/Documentos/{id}/Borradores")]
        public async Task<ActionResult<Unit>> UpdateDocumentoDraftAsync([FromRoute] int id, [FromBody] UpdateDocumentoSolicitudCertificacionDraftCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("Solicitudes/Documentos/{id}")]
        public async Task<ActionResult<Unit>> UpdateAsync([FromRoute] int id, [FromBody] UpdateDocumentoSolicitudCertificacionCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("{id}/Solicitudes")]
        public async Task<ActionResult<int>> PostAsync([FromBody] CreateSolicitudCertificacionCommand command, [FromRoute] int id)
        {
            // TODO: el id del socio es un parametro del querystring
            //var currentSocioId = _currentSocioService.GetCurrentEmpresaPortalId();

            //command.SocioId = (int)currentSocioId;
            command.CertificacionId = id;
            command.OrigenId = Origen.BACKOFFICE;
            command.PropietarioId = Origen.BACKOFFICE;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            // Replace 'Unit' with the specific result type for the command
            throw new NotImplementedException();
        }


        [HttpDelete("Solicitudes/{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id, [FromQuery] byte[] rowVersion)
        {
            var cmd = new DeleteSolicitudCertificacionCommand() { Id = id, RowVersion = rowVersion };
            return await _mediator.Send(cmd);
        }


        [HttpDelete("Solicitudes/Documentos/{id}")]
        public async Task<ActionResult<Unit>> DeleteDocumentoAsync([FromRoute] int id, [FromQuery] byte[] rowVersion)
        {
            var cmd = new DeleteDocumentoSolicitudCertificacionCommand() { Id = id, RowVersion = rowVersion };
            return await _mediator.Send(cmd);
        }


    }
}
