using DocumentFormat.OpenXml.Office2010.Excel;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.UserActivities.Dto;
using GSF.Application.Security.UserActivities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Empresas
{
    [Route("api/Proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class RolesTiposController : Controller
    {
        private readonly IMediator _mediator;

        public RolesTiposController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<RolTipoDto>>>
            GetAllAsync([FromQuery] GetManyRolesTiposQuery query)
        {
            var rolesTipos = await _mediator.Send(query);
            return Ok(rolesTipos);
        }

        [HttpGet("EmpresaRoles/{id}")]
        public async Task<ActionResult<IPaginatedQueryResult<EmpresaRolDto>>>
            GetAllAsync([FromRoute] int id)
        {
            GetEmpresasRolesTiposQuery query = new() { EmpresaPortalId = id };
            var empresas = await _mediator.Send(query);

            if (empresas == null)
            {
                return NotFound();
            }

            return Ok(empresas);
        }
    }
}
