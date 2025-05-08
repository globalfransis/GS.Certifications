using GS.Certifications.Application.Commons.Dtos;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Impuestos
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class ImpuestosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentCompanyService currentCompanyService;

        public ImpuestosController(IMediator mediator, ICurrentCompanyService currentCompanyService)
        {
            _mediator = mediator;
            this.currentCompanyService = currentCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImpuestoDto>>> GetAllAsync([FromQuery] GetImpuestosQuery query)
        {
            var currentCompanyId = await currentCompanyService.GetCurrentCompanyIdLazyAsync();
            query.CompanyId = currentCompanyId;
            return Ok(await _mediator.Send(query));
        }
    }
}
