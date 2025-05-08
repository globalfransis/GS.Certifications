using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Groups.Queries.GetGroupsByCurrentGroupOwnerAndOrganization;
using GSF.Application.Security.Organizations.Queries.GetOrganizationsByUserAndGroup;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Users.Queries.GetUserCrud;
using GSF.Application.Segurity.Companies.Queries.GetCompaniesByOrganizationAndUserId;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Socios.Web.Areas.Security.Pages.Users;


public class UserCrudModel : BasePageModel
{
    [BindProperty] public string Mode { get; set; } = "";

    [BindProperty] public long Id { get; set; }
    //[Required(ErrorMessage = "El nombre es obligatorio")]

    [Display(Name = "Nombre de Usuario")]
    [BindProperty] public string Login { get; set; }

    [Display(Name = "Nombre")]
    [BindProperty] public string FirstName { get; set; }

    [Display(Name = "Apellido")]
    [BindProperty] public string LastName { get; set; }

    [Display(Name = "Documento")]
    [BindProperty] public string IdNumber { get; set; }

    [Display(Name = "Clave")]
    [BindProperty] public string Password { get; set; }

    [Display(Name = "Teléfono")]
    [BindProperty] public string PhoneNumber { get; set; }

    [BindProperty] public string Email { get; set; }

    [DisplayName("Administrador")]
    [BindProperty] public bool SystemUse { get; set; }

    [DisplayName("Grupo")]
    [BindProperty] public List<UserCrudCompanyUserGroupDto> CompaniesUsersGroups { get; set; } = new List<UserCrudCompanyUserGroupDto>();

    //Datos de la vista pero que no representan a la entidad.
    [Display(Name = "Organización")]
    [BindProperty] public long OrganizationId { get; set; }
    public IEnumerable<SelectListItem> OrganizationsSelectList { get; set; } = new List<SelectListItem>();
    [Display(Name = "Empresa")]
    [BindProperty] public long CompanyId { get; set; }
    public IEnumerable<SelectListItem> CompaniesSelectList { get; set; } = new List<SelectListItem>();
    [Display(Name = "Grupo")]
    [BindProperty] public long GroupId { get; set; }
    public IEnumerable<SelectListItem> GroupsSelectList { get; set; } = new List<SelectListItem>();
    [BindProperty] public long LoggedUserId { get; set; }
    [BindProperty] public bool? LoggedUserSystemUse { get; set; }
    [HiddenInput][BindProperty] public byte[] RowVersion { get; set; } = new byte[] { 0 }; //Must be initializated, if not the binding in the create page (using a partialView) will have a modelState invalid.
    [BindProperty] public long CurrentGroupId { get; set; }
    [BindProperty] public long CurrentCompanyId { get; set; }

    protected readonly IMapper _mapper;
    protected readonly ICurrentCompanyService _currentCompanyService;
    protected readonly ICurrentUserService _currentUserService;
    protected readonly IStringLocalizer<Shared> _loc;

    public UserCrudModel(IMapper mapper,
                         ICurrentCompanyService currentCompanyService,
                         ICurrentUserService currentUserService,
                         IStringLocalizer<Shared> loc)
    {
        _mapper = mapper;
        _currentCompanyService = currentCompanyService;
        _currentUserService = currentUserService;
        _loc = loc;
    }

    protected async Task<UserCrudDto> LoadUser(long id)
    {
        var query = new GetUserCrudQuery() { Id = id };
        UserCrudDto userCrudDto = await Mediator.Send(query);

        if (userCrudDto == null)
            ErrorMessage = _loc["El usuario ya no existe"];
        else
            _mapper.Map(userCrudDto, this);
        return userCrudDto;
    }

    protected async Task SetRelationshipStatus()
    {
        ///Other way to do this is to load a List of 'SecurityUserCompaniesDto' from SecuritySessionStoreService.GetUserCompaniesAsync() and ask if for each company has the group equal to the current group.
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;
        CurrentCompanyId = CurrentCompanyId == default ? (await _currentCompanyService.GetCurrentCompanyAsync()).Id : CurrentCompanyId;
        LoggedUserId = LoggedUserId == default ? _currentUserService.UserId : LoggedUserId;
        foreach (var currentCug in CompaniesUsersGroups)
        {
            bool modifiable = currentCug.GroupGroupOwerId == CurrentGroupId && currentCug.CompanyId == CurrentCompanyId && LoggedUserId != Id || currentCug.Modifiable;
            if (modifiable)
                currentCug.Modifiable = true;
        }
    }

    protected async Task LoadControls()
    {
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;
        CurrentCompanyId = CurrentCompanyId == default ? (await _currentCompanyService.GetCurrentCompanyAsync()).Id : CurrentCompanyId;
        await LoadOrganizations();
        await LoadCompanies();
        await LoadGroups();
    }

    protected async Task LoadGroups()
    {
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;
        var query = new GetGroupsByCurrentGroupOwnerAndOrganizationQuery();
        List<SecurityGroupDto> groupDto = await Mediator.Send(query);
        GroupsSelectList = groupDto.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name
        }).ToList();
    }

    /// <summary>
    /// Filter the Companies by the OrganizationId selected.
    /// </summary>
    /// <returns></returns>
    protected async Task LoadCompanies()
    {
        var query = new GetCompaniesByOrganizationAndUserIdQuery()
        {
            OrganizationId = OrganizationId,
            UserId = LoggedUserId,
        };
        List<SecurityUserCompaniesDto> companyDto = await Mediator.Send(query);
        CompaniesSelectList = companyDto.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name
        }).ToList();
    }

    /// <summary>
    /// Removes from CompaniesSelectList the Companies already selected.
    /// </summary>
    protected void RemoveCompaniesInUse()
    {
        CompaniesSelectList = CompaniesSelectList.Where(cug => !CompaniesUsersGroups.Select(cug => cug.CompanyId).Contains(int.Parse(cug.Value))).ToList();
    }

    /// <summary>
    /// Load in the SelectList the Organizations de CurrentUser can operate.
    /// </summary>
    /// <returns></returns>
    protected async Task LoadOrganizations()
    {
        var query = new GetOrganizationsByCurrentUserAndGroupOwnerQuery();
        List<SecurityOrganizationDto> organizationDto = await Mediator.Send(query);
        OrganizationsSelectList = organizationDto.Select(org => new SelectListItem
        {
            Value = org.Id.ToString(),
            Text = org.Name,
        }).ToList();
    }

    public async Task<IActionResult> OnPostOrganizationsAsync()
    {
        await LoadControls();
        RemoveCompaniesInUse();
        return await Task.FromResult(Page());
    }

    public async Task<IActionResult> OnPostRemoveAsync(int index)
    {
        CompaniesUsersGroups.RemoveAt(index);
        await LoadControls();
        RemoveCompaniesInUse();
        ModelState.Clear();
        return await Task.FromResult(Page());
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCrudDto, UserCrudModel>();
        }
    }
}
