using GSF.Application.Companies;
using GSF.Application.Companies.Commands.AddCompanyCurrency;
using GSF.Application.Companies.Commands.CreateCompany;
using GSF.Application.Companies.Commands.DeleteCompany;
using GSF.Application.Companies.Commands.DeleteCompanyCurrency;
using GSF.Application.Companies.Commands.UpdateCompany;
using GSF.Application.Companies.CompanyCurrencies.Queries;
using GSF.Application.Companies.Queries;
using GSF.Application.Companies.Queries.GetCompaniesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Companies;

[Route("api/Companies/[controller]")]
[ApiController]
[AuthorizationGSF]
public class CompaniesController : Controller
{
    private readonly IMediator _mediator;
    public CompaniesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("GetCompanies")]
    public async Task<List<CompanyListItemDto>> GetAsync([FromQuery] GetCompaniesListQuery query) => await _mediator.Send(query);



    [HttpGet("GetCompany")]
    public async Task<CompanyDto> GetAsync([FromQuery] GetCompanyQuery query) => await _mediator.Send(query);

    [HttpPost("CreateCompany")]
    public async Task<long> Post([FromBody] CreateCompanyCommand command)
    {
        var result = await _mediator.Send(command);

        return result;
    }

    [HttpPut("UpdateCompany")]
    public async Task<IActionResult> Put([FromBody] UpdateCompanyCommand command)
    {
        _ = await _mediator.Send(command);

        IActionResult result = new StatusCodeResult((int)HttpStatusCode.OK);

        return result;
    }

    [HttpDelete("DeleteCompany")]
    public async Task<IActionResult> Delete([FromBody] DeleteCompanyCommand command)
    {
        _ = await _mediator.Send(command);

        IActionResult result = new StatusCodeResult((int)HttpStatusCode.OK);

        return result;
    }

    [HttpPost("AddNewCompanyCurrency")]
    public async Task<List<CompanyCurrencyDto>> AddCompanyCurrency([FromBody] AddCompanyCurrencyCommand command) => await _mediator.Send(command);

    [HttpDelete("DeleteCompanyCurrency")]
    public async Task<List<CompanyCurrencyDto>> DeleteCompanyCurrency([FromBody] DeleteCompanyCurrencyCommand command) => await _mediator.Send(command);
}