using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Impuestos
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class TiposImpuestosController
    {
        private readonly IMediator _mediator;

        public TiposImpuestosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ImpuestoTipoDto>>
            GetAllImpuestosTipos([FromQuery] GetImpuestosTiposQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
