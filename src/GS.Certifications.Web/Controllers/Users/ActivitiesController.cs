using GS.Certifications.Web.Controllers.Common.Attributes;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.UserActivities.Dto;
using GSF.Application.Security.UserActivities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Users
{
    [Route("api/Users/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class ActivitiesController : Controller
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IPaginatedQueryResult<UserActivityListDto>>>
            GetAllAsync([FromQuery] GetUserActivitiesQuery query)
        {
            var actividades = await _mediator.Send(query);
            return Ok(actividades);
        }
    }
}
