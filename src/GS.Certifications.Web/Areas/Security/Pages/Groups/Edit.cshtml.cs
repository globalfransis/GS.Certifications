using AutoMapper;
using GSF.Application.Security.Groups.Commands.UpdateGroup;
using GSF.Application.Security.Groups.Queries.GetGroupCrud;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Groups;

public class EditGrupModel : GroupCrudModel
{

    #region ctor/private

    public EditGrupModel(IMapper mapper,
                         ICurrentCompanyService currentCompanyService,
                         IStringLocalizer<Shared> loc) : base(mapper, currentCompanyService, loc)
    {
        Title = loc["Modificar Grupo"];
        Mode = "Modificación";
    }
    #endregion

    public async Task<IActionResult> OnGet(int id)
    {
        IActionResult result = null;
        GroupCrudDto groupDto = await LoadGroup(id);
        if (groupDto == null)
        {
            ErrorMessage = _loc["El Grupo ya no existe"];
            result = RedirectByModelState("/Groups/Index", new { area = "Security" });
        }
        else
        {
            await LoadControls();
            UpdateSelectLists();
            CurrentOrganizationId = (await _currentCompanyService.GetCurrentCompanyOrganizationAsync()).Id;
        }
        return result ?? Page();
    }

    public async Task<IActionResult> OnPostSave()
    {
        IActionResult result;
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;

        var command = new UpdateGroupCommand()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            GroupOwnerId = CurrentGroupId,
            GroupRoles = from r in GroupRoles
                         select r.RoleId,
            GroupsOrganizations = from o in GroupsOrganizations
                                  select o.OrganizationId,
            RowVersion = RowVersion
        };

        try
        {
            await LoadControls();
            UpdateSelectLists();

            await Mediator.Send(command);
            SuccessMessage = _loc["Se ha modificado el grupo {0}", Name];
            result = RedirectByModelState("/Groups/Detail", new { area = "Security", id = Id });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/Groups/Index", new { area = "Security" });
        }

        return result;
    }
}
