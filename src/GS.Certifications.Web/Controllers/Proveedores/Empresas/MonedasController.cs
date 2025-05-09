using GSF.Application.Global.Currencies.Queries;
using GSF.Application.Helpers.Pagination.Interfaces;
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
    [Route("api/proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class MonedasController : Controller
    {
        private readonly IMediator _mediator;

        public MonedasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CurrencyDto>>
            GetAllMonedas([FromQuery] GetCurrenciesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
