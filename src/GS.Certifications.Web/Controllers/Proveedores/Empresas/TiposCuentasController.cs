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
    public class TiposCuentasController : Controller
    {
        private readonly IMediator _mediator;

        public TiposCuentasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<TipoCuentaDto>>
            GetAllTiposCuentas([FromQuery] GetTiposCuentasQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
