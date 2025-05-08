using GS.Certifications.Application.Commons.Dtos;
using GS.Certifications.Application.UseCases.Percepciones.Queries;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Percepciones
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationGSF]
    public class PercepcionesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentCompanyService currentCompanyService;

        public PercepcionesController(IMediator mediator, ICurrentCompanyService currentCompanyService)
        {
            _mediator = mediator;
            this.currentCompanyService = currentCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PercepcionDto>>> GetAllAsync([FromQuery] GetPercepcionesQuery query)
        {
            var currentCompanyId = await currentCompanyService.GetCurrentCompanyIdLazyAsync();
            query.CompanyId = currentCompanyId;
            return await _mediator.Send(query);
        }
    }
}
