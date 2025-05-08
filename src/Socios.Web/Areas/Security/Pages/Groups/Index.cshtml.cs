using AutoMapper;
using GSF.Application.Segurity.Companies.Queries.GetGroupsByCurrentGroupOwner;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Common.Session;
using Socios.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Socios.Web.Areas.Security.Pages.Groups;

public class GroupsIndexModel : BasePageModel
{
    protected readonly IStringLocalizer<Shared> _loc;
    private readonly IParametersSessionStoreService _parametersSessionStoreService;
    protected readonly IMapper _mapper;
    public bool IsPostBack { get; set; } = false;
    public string NoDataMessage { get; private set; }


    public GroupsIndexModel(IMapper mapper, IParametersSessionStoreService parametersSessionStoreService, IStringLocalizer<Shared> loc)
    {
        Title = loc["Grupos"];
        _loc = loc;
        _mapper = mapper;
        _parametersSessionStoreService = parametersSessionStoreService;
    }
    [BindProperty] public List<GroupListIndexViewModel> Groups { get; private set; }
    [Display(Name = "Nombre")]
    [BindProperty(SupportsGet = true)] public string SearchName { get; set; }
    [Display(Name = "Organización")]
    [BindProperty(SupportsGet = true)] public string SearchOrganization { get; set; }
    [Display(Name = "Rol")]
    [BindProperty(SupportsGet = true)] public string SearchRole { get; set; }


    private void SetNoDataMessage()
    {
        NoDataMessage = IsPostBack ? _loc["No hay datos"] : _loc["Click en 'Buscar' para traer resultados"];
    }
    public async Task OnGet()
    {
        await GetParametersFromSession();
        SetNoDataMessage();
        var query = new GetGroupsByCurrentGroupOwnerQuery()
        {
            Name = SearchName ?? "",
            OrganizationName = SearchOrganization ?? "",
            RoleName = SearchRole ?? "",
        };
        var queryResult = await Mediator.Send(query);
        Groups = _mapper.Map<List<GroupListDto>, List<GroupListIndexViewModel>>(queryResult.Distinct().ToList());
    }

    public async Task OnGetSearch()
    {
        IsPostBack = true;
        SetNoDataMessage();
        await SaveParametersInSession();
        var query = new GetGroupsByCurrentGroupOwnerQuery()
        {
            Name = SearchName ?? "",
            OrganizationName = SearchOrganization ?? "",
            RoleName = SearchRole ?? "",
        };
        var queryResult = await Mediator.Send(query);
        Groups = _mapper.Map<List<GroupListDto>, List<GroupListIndexViewModel>>(queryResult.Distinct().ToList());
    }

    private async Task GetParametersFromSession()
    {
        dynamic parameters = await _parametersSessionStoreService.GetParametersAsync("groups");

        if (parameters != null)
        {
            SearchName = parameters.SearchName;
            SearchOrganization = parameters.SearchOrganization;
            SearchRole = parameters.SearchRole;
        }
    }

    private async Task SaveParametersInSession()
    {
        dynamic par = new
        {
            SearchName,
            SearchOrganization,
            SearchRole
        };

        string key = "groups";

        await _parametersSessionStoreService.SaveParametersAsync(par, key);
    }

    public class GroupListIndexViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrganizationsNames { get; set; }
        public string RolesNames { get; set; }
        public string GroupOwnerName { get; set; }
        public DateTime? Modified { get; set; }
        public byte[] RowVersion { get; set; }
        public bool SystemUse { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<GroupListDto, GroupListIndexViewModel>()
                    .ForMember(dst => dst.OrganizationsNames, opt => opt.MapFrom(src => string.Join(", ", src.Organizations.Select(go => go).Distinct())))
                    .ForMember(dst => dst.RolesNames, opt => opt.MapFrom(src => string.Join(", ", src.Roles.Select(gr => gr).Distinct())));
            }
        }
    }
}