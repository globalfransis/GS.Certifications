using GSF.Application.Global.Currencies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Empresas
{
    [Route("api/proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class ModosLecturasController
    {
        private readonly IMediator _mediator;

        public ModosLecturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ModoLecturaDto>>
            GetAllMonedas([FromQuery] GetModosLecturasQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
