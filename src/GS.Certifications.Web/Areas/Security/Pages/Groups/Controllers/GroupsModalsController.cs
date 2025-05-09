using AutoMapper;
using GSF.Application.Security.Groups.Queries.GetGroupCrud;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Areas.Security.Pages.Groups.Modals;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Groups.Controllers;

public class GroupsModalsController : Controller
{
    private IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentCompanyService _currentCompanyService;
    private readonly IStringLocalizer<Shared> _loc;

    public GroupsModalsController(IMapper mapper,
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
    [Route("/GroupsModals/DelegationModal")]
    public async Task<IActionResult> OnGetDelegationModalPartialAsync(long id)
    {
        var query = new GetGroupCrudQuery() { Id = id };
        GroupCrudDto groupCRUDDto = await _mediator.Send(query);

        var partialViewModel = new GroupDelegationPartialModel(_currentCompanyService, _mediator);

        if (groupCRUDDto == null)
            return NotFound(new { message = _loc["Grupo no encontrado."] });
        else
            _mapper.Map(groupCRUDDto, partialViewModel);

        await partialViewModel.Initialize();

        return PartialView("~/Areas/Security/Pages/Groups/Modals/_DelegationModalPartial.cshtml", partialViewModel);
    }
}
