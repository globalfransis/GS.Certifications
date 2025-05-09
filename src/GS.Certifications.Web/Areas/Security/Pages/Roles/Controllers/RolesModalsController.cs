using AutoMapper;
using GSF.Application.Security.Roles.Queries.GetRoleCrudQuery;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Areas.Security.Pages.Roles.Modals;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Roles.Controllers;

public class RolesModalsController : Controller
{
    private IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentCompanyService _currentCompanyService;
    private readonly IStringLocalizer<Shared> _loc;

    public RolesModalsController(IMapper mapper,
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
    [Route("/RolesModals/DelegationModal")]
    public async Task<IActionResult> OnGetDelegationModalPartialAsync(long id)
    {
        var query = new GetRoleCrudQuery() { Id = id };
        RoleCrudDto roleCRUDDto = await _mediator.Send(query);

        var partialViewModel = new RoleDelegationPartialModel(_currentCompanyService, _mediator);

        if (roleCRUDDto == null)
            return NotFound(new { message = _loc["Rol no encontrado."] });
        else
            _mapper.Map(roleCRUDDto, partialViewModel);

        await partialViewModel.Initialize();

        return PartialView("~/Areas/Security/Pages/Roles/Modals/_DelegationModalPartial.cshtml", partialViewModel);
    }
}
