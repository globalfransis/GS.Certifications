using AutoMapper;
using GSF.Application.Security.Groups.Queries.GetGroupCrud;
using GSF.Application.Security.Organizations.Queries.GetOrganizationsByUserAndGroup;
using GSF.Application.Security.Roles.Queries.GetRolesByCurrentGroupOwner;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static GSF.Application.Security.Groups.Queries.GetGroupCrud.GroupCrudDto;

namespace Socios.Web.Areas.Security.Pages.Groups;


public class GroupCrudModel : BasePageModel
{
    [BindProperty] public string Mode { get; set; } = "";
    [BindProperty] public long Id { get; set; }
    [Display(Name = "Nombre")]
    [BindProperty] public string Name { get; set; }
    [Display(Name = "Descripción")]
    [BindProperty] public string Description { get; set; }
    [BindProperty] public bool SystemUse { get; set; }
    [BindProperty] public List<GroupCrudGroupRoleDto> GroupRoles { get; set; } = new List<GroupCrudGroupRoleDto>();
    [BindProperty] public List<GroupCrudGroupsOrganizationsDto> GroupsOrganizations { get; set; } = new List<GroupCrudGroupsOrganizationsDto>();
    public List<SecurityOrganizationDto> OrganizationsList { get; set; } = new List<SecurityOrganizationDto>();
    public List<RoleListDto> RoleList { get; set; } = new List<RoleListDto>();
    //Datos de la vista pero que no representan a la entidad.
    [Display(Name = "Organización")]
    [BindProperty] public long SelectedOrganizationId { get; set; }
    public IEnumerable<SelectListItem> OrganizationsSelectList { get; set; } = new List<SelectListItem>();
    [Display(Name = "Role")]
    [BindProperty] public long SelectedRoleId { get; set; }
    public IEnumerable<SelectListItem> RoleSelectList { get; set; } = new List<SelectListItem>();
    [BindProperty] public long CurrentOrganizationId { get; set; }
    [BindProperty] public long CurrentGroupId { get; set; }
    [Display(Name = "Grupo Dueño")]
    [BindProperty] public string GroupOwnerName { get; set; }
    [HiddenInput][BindProperty] public byte[] RowVersion { get; set; } = new byte[] { 0 }; //Must be initializated, if not the binding in the create page (using a partialView) will have a modelState invalid.


    protected readonly IMapper _mapper;
    protected readonly ICurrentCompanyService _currentCompanyService;
    protected readonly IStringLocalizer<Shared> _loc;

    public GroupCrudModel(IMapper mapper,
                          ICurrentCompanyService currentCompanyService,
                          IStringLocalizer<Shared> loc)
    {
        _mapper = mapper;
        _currentCompanyService = currentCompanyService;
        _loc = loc;
    }

    protected async Task<GroupCrudDto> LoadGroup(int id)
    {
        var query = new GetGroupCrudQuery() { Id = id };
        GroupCrudDto groupDto = await Mediator.Send(query);

        //Ver si cambiamos para que la validacion en la query y devuelva una excepcion.
        if (groupDto == null)
            ErrorMessage = _loc["El Grupo ya no existe"];
        else
            _mapper.Map(groupDto, this);
        return groupDto;
    }

    protected async Task LoadControls()
    {
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;
        await LoadOrganizations();
        await LoadRoles();
    }

    /// <summary>
    /// Load in the SelectList the Organizations de CurrentUser can operate.
    /// </summary>
    /// <returns></returns>
    protected async Task LoadOrganizations()
    {
        var query = new GetOrganizationsByCurrentUserAndGroupOwnerQuery();
        OrganizationsList = await Mediator.Send(query);
        OrganizationsSelectList = OrganizationsList.Select(org => new SelectListItem
        {
            Value = org.Id.ToString(),
            Text = org.Name,
        }).ToList();
    }

    /// <summary>
    /// Load in the SelectList the Organizations de CurrentUser (with the CurrentCompany) can operate.
    /// </summary>
    /// <returns></returns>
    protected async Task LoadRoles()
    {
        var query = new GetRolesByCurrentGroupIdFilteredQuery();
        RoleList = await Mediator.Send(query);

        //The SystemUseRole never must be binded. Only one group with a SystemUseRole must exists and its role cant be edited.
        RoleList = RoleList.Where(r => !r.SystemUse).ToList();

        RoleSelectList = RoleList.Select(org => new SelectListItem
        {
            Value = org.Id.ToString(),
            Text = org.Name,
        }).ToList();
    }

    /// <summary>
    /// Get the combo list cleaned.
    /// </summary>
    /// <returns></returns>
    protected void UpdateSelectLists()
    {
        OrganizationsSelectList = OrganizationsSelectList.Where(sl => !GroupsOrganizations.Any(go => go.OrganizationId.ToString() == sl.Value)).ToList();
        RoleSelectList = RoleSelectList.Where(sl => !GroupRoles.Any(gr => gr.RoleId.ToString() == sl.Value)).ToList();
    }

    public async Task<IActionResult> OnPostAddOrganizationAsync()
    {
        await LoadControls();

        SecurityOrganizationDto selectedOrganization = OrganizationsList.FirstOrDefault(o => o.Id == SelectedOrganizationId);

        if (!GroupsOrganizations.Where(go => go.OrganizationId == SelectedOrganizationId).Any() && selectedOrganization != null)
        {
            GroupCrudGroupsOrganizationsDto relationship = new GroupCrudGroupsOrganizationsDto()
            {
                OrganizationId = selectedOrganization.Id,
                OrganizationName = selectedOrganization.Name,
            };

            GroupsOrganizations.Add(relationship);
        }
        else
            ErrorMessage = _loc["Ya existe una relación con la Organización seleccionada."];

        UpdateSelectLists();
        return await Task.FromResult(Page());
    }

    public async Task<IActionResult> OnPostAddRoleAsync()
    {
        await LoadControls();

        RoleListDto selectedRole = RoleList.FirstOrDefault(r => r.Id == SelectedRoleId);

        if (!GroupRoles.Where(gr => gr.RoleId == SelectedRoleId).Any() && selectedRole != null)
        {
            GroupCrudGroupRoleDto relationship = new GroupCrudGroupRoleDto()
            {
                RoleId = selectedRole.Id,
                RoleName = selectedRole.Name,
                RoleDescription = selectedRole.Description
            };

            GroupRoles.Add(relationship);
        }
        else
            ErrorMessage = _loc["Ya existe una relación con el Rol seleccionado."];

        UpdateSelectLists();
        return await Task.FromResult(Page());
    }

    public async Task<IActionResult> OnPostRemoveOrganizationAsync(int organizationIndex)
    {
        GroupsOrganizations.RemoveAt(organizationIndex);
        await LoadControls();
        UpdateSelectLists();
        ModelState.Clear();
        return await Task.FromResult(Page());
    }

    public async Task<IActionResult> OnPostRemoveRoleAsync(int roleIndex)
    {
        GroupRoles.RemoveAt(roleIndex);
        await LoadControls();
        UpdateSelectLists();
        ModelState.Clear();
        return await Task.FromResult(Page());
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GroupCrudDto, GroupCrudModel>();
        }
    }
}
