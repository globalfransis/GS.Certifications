using AutoMapper;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Users.Queries.GetUserCrud;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Areas.Security.Pages.Users.Modals;

namespace Socios.Web.Areas.Security.Pages.Users.Controllers;

public class UsersModalsController : Controller
{
    private IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentCompanyService _currentCompanyService;
    private readonly IStringLocalizer<Shared> _loc;

    public UsersModalsController(IMapper mapper,
                                ICurrentCompanyService currentCompanyService,
                                IMediator mediator,
                                IStringLocalizer<Shared> loc)
    {
        _mapper = mapper;
        _currentCompanyService = currentCompanyService;
        _mediator = mediator;
        _loc = loc;
    }

    [HttpGet]
    [Route("/UsersModals/DelegationModal")]
    public async Task<IActionResult> OnGetDelegationModalPartialAsync(long id)
    {

        var query = new GetUserCrudQuery() { Id = id };
        UserCrudDto userCrudDto = await _mediator.Send(query);

        var partialViewModel = new UserDelegationPartialModel(_currentCompanyService, _mediator);

        if (userCrudDto == null)
            return NotFound(new { message = _loc["Usuario no encontrado."] });
        else
            _mapper.Map(userCrudDto, partialViewModel);

        await partialViewModel.Initialize();

        return PartialView("~/Areas/Security/Pages/Users/Modals/_DelegationModalPartial.cshtml", partialViewModel);
    }

}
