using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Services;
using GSF.Application.Security.Users.Queries;
using GSF.Application.Security.Users.Queries.GetUsersByCurrentGroupOwner;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Common.Session;
using GS.Certifications.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Users;

public class UsersIndexModel : BasePageModel
{
    protected readonly IMapper _mapper;
    private readonly IStringLocalizer<Shared> _loc;
    private readonly ICurrentUserService _currentUserService;
    private readonly IParametersSessionStoreService _parametersSessionStoreService;
    public bool IsPostBack { get; set; } = false;
    public string NoDataMessage { get; private set; }

    public UsersIndexModel(IMapper mapper, IParametersSessionStoreService parametersSessionStoreService, IStringLocalizer<Shared> loc, ICurrentUserService currentUserService)
    {
        Title = loc["Usuarios"];
        _mapper = mapper;
        _loc = loc;
        _currentUserService = currentUserService;
        _parametersSessionStoreService = parametersSessionStoreService;
    }
    [BindProperty] public List<UserListIndexViewModel> Users { get; private set; }
    [Display(Name = "Nombre de Usuario")]
    [BindProperty(SupportsGet = true)] public string SearchLogin { get; set; }
    [Display(Name = "Email")]
    [BindProperty(SupportsGet = true)] public string SearchEmail { get; set; }
    [Display(Name = "Organización")]
    [BindProperty(SupportsGet = true)] public string SearchOrganization { get; set; }
    [Display(Name = "Empresa")]
    [BindProperty(SupportsGet = true)] public string SearchCompany { get; set; }
    [Display(Name = "Grupo")]
    [BindProperty(SupportsGet = true)] public string SearchGroup { get; set; }
    [BindProperty] public long LoggedUserId { get; set; }

    public string DefaultLanguage { get; set; }

    private void SetNoDataMessage()
    {
        NoDataMessage = IsPostBack ? _loc["No hay datos"] : _loc["Click en 'Buscar' para traer resultados"];
    }
    public async Task OnGet()
    {
        await GetParametersFromSession();
        LoggedUserId = LoggedUserId == default ? _currentUserService.UserId : LoggedUserId;
        SetNoDataMessage();
        var query = new GetUsersByCurrentGroupOwnerQuery()
        {
            Login = SearchLogin ?? "",
            Email = SearchEmail ?? "",
            OrganizationName = SearchOrganization ?? "",
            CompanyName = SearchCompany ?? "",
            GroupName = SearchGroup ?? "",
        };
        var queryResult = await Mediator.Send(query);
        Users = _mapper.Map<List<UserListDto>, List<UserListIndexViewModel>>(queryResult);
    }

    public async Task OnGetSearch()
    {
        IsPostBack = true;
        await SaveParametersInSession();
        SetNoDataMessage();
        LoggedUserId = LoggedUserId == default ? _currentUserService.UserId : LoggedUserId;
        var query = new GetUsersByCurrentGroupOwnerQuery()
        {
            Login = SearchLogin ?? "",
            Email = SearchEmail ?? "",
            OrganizationName = SearchOrganization ?? "",
            CompanyName = SearchCompany ?? "",
            GroupName = SearchGroup ?? "",
        };
        var queryResult = await Mediator.Send(query);
        Users = _mapper.Map<List<UserListDto>, List<UserListIndexViewModel>>(queryResult);
    }

    private async Task GetParametersFromSession()
    {
        dynamic parameters = await _parametersSessionStoreService.GetParametersAsync("users");

        if (parameters != null)
        {
            SearchLogin = parameters.SearchLogin;
            SearchEmail = parameters.SearchEmail;
            SearchOrganization = parameters.SearchOrganization;
            SearchCompany = parameters.SearchCompany;
            SearchGroup = parameters.SearchGroup;
        }
    }

    private async Task SaveParametersInSession()
    {
        dynamic par = new
        {
            SearchLogin,
            SearchEmail,
            SearchOrganization,
            SearchCompany,
            SearchGroup
        };

        string key = "users";

        await _parametersSessionStoreService.SaveParametersAsync(par, key);
    }

    //public async Task<IActionResult> OnGetDelegationModalPartialAsync(long id)
    //{
    //    var query = new GetUserCrudQuery() { Id = id };
    //    UserCrudDto userCRUDDto = await Mediator.Send(query);

    //    var partialViewModel = new UserDelegationPartialModel();

    //    if (userCRUDDto == null)
    //        //ErrorMessage = "El usuario ya no existe";
    //        throw new System.Exception("El usuario ya no existe");
    //    else
    //        _mapper.Map(userCRUDDto, partialViewModel);

    //    var queryGroups = new GetGroupsByCurrentGroupOwnerQuery();
    //    List<GroupListDto> groupDto = await Mediator.Send(queryGroups);
    //    partialViewModel.GroupsSelectList = groupDto.Select(a => new SelectListItem
    //    {
    //        Value = a.Id.ToString(),
    //        Text = a.Name
    //    }).ToList();

    //    partialViewModel.CurrentGroupOwnerId = (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id;
    //    partialViewModel.SelectedGroupOwnerId = partialViewModel.CurrentGroupOwnerId;

    //    return Partial("Modals/_DelegationModalPartial", partialViewModel);
    //}

    public class UserListIndexViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Modified { get; set; }
        public byte[] RowVersion { get; set; }
        public bool SystemUse { get; set; }
        public SecurityGroupDto GroupOwner { get; set; }

        public string CompaniesNames { get; set; }
        public string GroupsNames { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<UserListDto, UserListIndexViewModel>()
                    .ForMember(dst => dst.CompaniesNames, opt => opt.MapFrom(src => string.Join(", ", src.Companies.Select(c => c).Distinct())))
                    .ForMember(dst => dst.GroupsNames, opt => opt.MapFrom(src => string.Join(", ", src.Groups.Select(g => g).Distinct())));
            }
        }
    }
}