using AutoMapper;
using GSF.Application.Security.Groups.Commands.CreateGroup;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using static GSF.Application.Security.Groups.Queries.GetGroupCrud.GroupCrudDto;

namespace GS.Certifications.Web.Areas.Security.Pages.Groups;

public class CreateGroupModel : GroupCrudModel
{
    #region ctor/private

    public CreateGroupModel(IMapper mapper,
                            ICurrentCompanyService currentCompanyService,
                            IStringLocalizer<Shared> loc) : base(mapper, currentCompanyService, loc)
    {
        Title = loc["Crear Grupo"];
        Mode = "Alta";
    }
    #endregion

    public async Task OnGet()
    {
        await LoadControls();
        await CreateMandatoryGroupOrganization();
        UpdateSelectLists();
    }

    public async Task CreateMandatoryGroupOrganization()
    {
        CurrentOrganizationId = (await _currentCompanyService.GetCurrentCompanyOrganizationAsync()).Id;

        SecurityOrganizationDto selectedOrganization = OrganizationsList.FirstOrDefault(o => o.Id == CurrentOrganizationId);

        if (GroupsOrganizations.Where(o => o.Id == SelectedOrganizationId).ToList().Count == 0 && selectedOrganization != null)
        {
            GroupCrudGroupsOrganizationsDto relationship = new GroupCrudGroupsOrganizationsDto()
            {
                OrganizationId = selectedOrganization.Id,
                OrganizationName = selectedOrganization.Name,
            };

            GroupsOrganizations.Add(relationship);
        }
        else
            ErrorMessage = _loc["Ya existe una relación con esa Organización."];
    }

    public async Task<IActionResult> OnPostSave()
    {
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;

        var command = new CreateGroupCommand()
        {
            Name = Name,
            Description = Description,
            GroupOwnerId = CurrentGroupId,
            GroupRoles = from r in GroupRoles
                         select r.RoleId,
            GroupsOrganizations = from o in GroupsOrganizations
                                  select o.OrganizationId,
        };

        await LoadControls();
        UpdateSelectLists();

        Id = await Mediator.Send(command);
        SuccessMessage = _loc["Se ha creado el grupo {0}", Name];
        return RedirectByModelState("/Groups/Detail", new { area = "Security", id = Id });
    }
}
