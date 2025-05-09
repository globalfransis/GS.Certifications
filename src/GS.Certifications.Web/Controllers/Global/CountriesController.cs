using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Parameters.Countries.Queries.GetCountries;
using GSF.Application.Parametrics.Geo.Provinces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Global
{
    [Route("api/Global/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class CountriesController : Controller
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CountryDto>>
            GetAllUserTypes([FromQuery] GetPaisesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
