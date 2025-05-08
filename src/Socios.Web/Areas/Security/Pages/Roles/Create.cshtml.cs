using AutoMapper;
using GSF.Application.Security.Roles.Commands.CreateRole;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace Socios.Web.Areas.Security.Pages.Roles;

public class CreateRoleModel : RoleCrudModel
{
    private readonly ICurrentCompanyService _currentCompanyService;
    #region ctor/private

    public CreateRoleModel(IMapper mapper,
                           ICurrentCompanyService currentCompanyService,
                           IStringLocalizer<Shared> loc) : base(mapper, loc)
    {
        Title = loc["Crear Rol"];
        Mode = "Alta";
        _currentCompanyService = currentCompanyService;
    }

    #endregion

    public async Task OnGet()
    {
        await GenerateOptionTree();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!string.IsNullOrEmpty(SelectedValues))
            GenerateListsForSave();

        CurrentGroupId = CurrentGroupId == default ? _currentCompanyService.GetCurrentCompanyGroupAsync().GetAwaiter().GetResult().Id : CurrentGroupId;

        var command = new CreateRoleCommand()
        {
            Name = Name,
            Description = Description,
            GroupOwnerId = CurrentGroupId,
            SelectedGrants = SelectedGrants.ToList(),
            SelectedOptions = SelectedOptions.ToList(),
        };

        Id = await Mediator.Send(command);

        SuccessMessage = _loc["Se ha creado el rol {0}", Name];
        return RedirectByModelState("/Roles/Detail", new { area = "Security", id = Id });
    }


}
