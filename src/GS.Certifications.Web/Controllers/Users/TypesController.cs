using GS.Certifications.Web.Controllers.Common.Attributes;
using GSF.Application.Security.UserActivities.Dto;
using GSF.Application.Security.UserActivities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class TypesController : Controller
    {
        private readonly IMediator _mediator;

        public TypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<UserTypeDto>>
            GetAllUserTypes([FromQuery] GetUserTypesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
