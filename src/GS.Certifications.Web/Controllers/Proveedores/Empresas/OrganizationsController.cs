using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Organizations.Queries.GetOrganizationsByUserAndGroup;
using GSF.Application.Security.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Empresas
{
    [Route("api/proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class OrganizationsController : Controller
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<SecurityOrganizationDto>> GetAllOrganizations ([FromQuery] GetOrganizationsByCurrentUserAndGroupOwnerQuery query)
        {
            // Replace 'IRequest' with the specific query type and 'object' with the result type
            return await _mediator.Send(query);
        }
    }
}
