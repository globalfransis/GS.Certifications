using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Parametrics.Geo.Provinces;
using GSF.Application.Security.UserActivities.Dto;
using GSF.Application.Security.UserActivities.Queries;
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
    public class ProvincesController : Controller
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ProvinceDto>>
            GetAllUserTypes([FromQuery] GetProvincesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
