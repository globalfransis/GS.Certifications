using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.ConceptosGastos;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.ModosLectura;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.OrdenesCompras;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.Usuarios;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using GS.Certifications.Domain.Commons.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Empresa.Administracion
{
    [Route("api/Proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class AdministracionController : Controller
    {
        private readonly IMediator _mediator;

        public AdministracionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaPortalDto>> GetOneAsync([FromRoute] long id)
        {
            GetEmpresaQuery query = new() { Id = id };
            var empresa = await _mediator.Send(query);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<EmpresaForListDto>>>
            GetAllAsync([FromQuery] GetEmpresasQuery query)
        {
            var empresas = await _mediator.Send(query);
            return Ok(empresas);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateAsync([FromBody] UpdateEmpresaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<long>> PostAsync([FromBody] CreateEmpresaCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id)
        {
            DeleteEmpresaCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("AñadirUsuarioExterno")]
        public async Task<ActionResult<Unit>>
            AgregarUsuarioExternoAsync([FromBody] AddUsuarioExternoToEmpresaPortalCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("ObtenerUsuariosExternos")]
        public async Task<List<UsuarioEmpresaPortalDto>>
            GetAllExternosAsync([FromQuery] GetUsuariosEmpresasQuery query)
        {
            query.TipoUsuario = UserTypeIdmConstants.Socio;
            return await _mediator.Send(query);
        }

        [HttpGet("ObtenerUsuario/{usuarioId}")]
        public async Task<ActionResult<UsuarioEmpresaPortalDto>>
            GetOneExternoAsync([FromRoute] int usuarioId)
        {
            GetUsuarioEmpresaQuery query = new() { UsuarioEmpresaPortalId = usuarioId };
            var usuario = await _mediator.Send(query);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPut("ModificarUsuario/{id}")]
        public async Task<ActionResult<Unit>> UpdateUsuarioExternoAsync([FromBody] UpdateUsuarioCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("EliminarUsuario/{id}")]
        public async Task<ActionResult<Unit>> DeleteUsuarioExternoAsync([FromRoute] int id)
        {
            DeleteUsuarioCommand command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("AñadirUsuarioInterno")]
        public async Task<ActionResult<Unit>>
            AgregarUsuarioInternoAsync([FromBody] AddUsuarioInternoToEmpresaPortalCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("ObtenerUsuariosInternos")]
        public async Task<List<UsuarioEmpresaPortalDto>>
            GetAllInternosAsync([FromQuery] GetUsuariosEmpresasQuery query)
        {
            query.TipoUsuario = UserTypeIdmConstants.Backend;
            return await _mediator.Send(query);
        }

        [HttpGet("ObtenerModosLectura")]
        public async Task<List<EmpresaModoLecturaDto>>
            GetAllEmpresasModosLecturasAsync([FromQuery] GetEmpresasModosLecturasQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("ModificarModosLectura")]
        public async Task<ActionResult<Unit>> ModificarModosLecturaAsync([FromBody] UpdateEmpresaModosLecturaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("ObtenerOrdenesComprasTipos")]
        public async Task<List<EmpresaOrdenCompraTipoDto>>
        GetAllEmpresasOrdenesComprasTiposAsync([FromQuery] GetEmpresasOrdenesComprasTiposQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("ModificarOrdenesComprasTipos")]
        public async Task<ActionResult<Unit>> ModificarOrdenesComprasTiposAsync
            ([FromBody] UpdateEmpresaOrdenesComprasTiposCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("ObtenerConceptosGastosTipos")]
        public async Task<List<EmpresaConceptoGastoTipoDto>>
        GetAllEmpresasConceptosGastosTiposAsync([FromQuery] GetEmpresasConceptosGastosTiposQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("ModificarConceptosGastosTipos")]
        public async Task<ActionResult<Unit>> ModificarConceptosGastosTiposAsync
            ([FromBody] UpdateEmpresaConceptosGastosTiposCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
