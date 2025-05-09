using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Users.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Users
{
    [Route("api/Proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Externo/{email}/Exists")]
        public async Task<ActionResult<bool>> GetOneExternoAsync([FromRoute] string email)
        {
            ValidarSiUsuarioExternoExisteQuery query = new() { Email = email };
            bool resultado = await _mediator.Send(query);
            return resultado;
        }

        [HttpGet("Interno/{email}/Exists")]
        public async Task<ActionResult<bool>> GetOneInternoAsync([FromRoute] string email)
        {
            ValidarSiUsuarioInternoExisteQuery query = new() { Email = email };
            bool resultado = await _mediator.Send(query);
            return resultado;
        }
    }
}
