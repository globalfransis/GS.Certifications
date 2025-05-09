using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.CategoriaTipos.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Empresas
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class CategoriaTiposController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriaTiposController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CategoriaTipoDto>>
            GetAllCategoriasTipos([FromQuery] GetCategoriaTiposQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
