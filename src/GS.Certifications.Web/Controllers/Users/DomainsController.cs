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
    public class DomainsController : Controller
    {
        private readonly IMediator _mediator;

        public DomainsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<DomainFsDto>>
            GetAllUserDomainFs([FromQuery] GetUserDomainFsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
