using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.Commons.Dtos.Percepciones;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using GS.Certifications.Application.UseCases.Percepciones.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Percepciones
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class TiposPercepcionesController : Controller
    {
        private readonly IMediator _mediator;

        public TiposPercepcionesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<PercepcionTipoDto>>
            GetAllPercepcionesTipos([FromQuery] GetPercepcionesTiposQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
