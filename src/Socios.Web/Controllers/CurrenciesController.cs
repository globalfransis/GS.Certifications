using GSF.Application.Global.Currencies.Queries;
using GSF.Application.Global.Currencies.Queries.GetCurrencies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class CurrenciesController : Controller
{
    private readonly IMediator _mediator;

    public CurrenciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CurrencyDto>>> GetAllAsync([FromQuery] GetCurrenciesQuery query)
    {
        return await _mediator.Send(query);
    }

}
