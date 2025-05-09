using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.CategoriaTipos.Queries;
using GS.Certifications.Application.UseCases.OrdenesCompras.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.OrdenesCompras
{
    [Route("api/proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class OrdenesComprasEstadosController : Controller
    {
        private readonly IMediator _mediator;

        public OrdenesComprasEstadosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<List<OrdenCompraEstadoDto>> GetAllAsync([FromQuery] GetListaOrdenesComprasEstadosQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
