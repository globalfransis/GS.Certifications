using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Parameters.Countries.Queries.GetCountries;
using GSF.Application.Parametrics.Geo.Cities;
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
    public class CitiesController : Controller
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CityDto>>
            GetAllUserTypes([FromQuery] GetCitiesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
