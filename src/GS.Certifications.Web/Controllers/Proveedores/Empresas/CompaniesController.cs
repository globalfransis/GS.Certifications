using DocumentFormat.OpenXml.Office2010.Excel;
using GSF.Application.Common.Interfaces;
using GSF.Application.Companies;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Companies.Queries.GetCompaniesByUser;
using GSF.Application.Security.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Web.Common.Services;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Empresas
{
    [Route("api/proveedores/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class CompaniesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _service;
        public CompaniesController(IMediator mediator, ICurrentUserService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpGet]
        public async Task<List<SecurityUserCompaniesDto>>
            GetAllCompanies()
        {
            GetCompaniesByUserQuery query = new() { UserId = _service.UserId };
            return await _mediator.Send(query);
        }
    }
}
