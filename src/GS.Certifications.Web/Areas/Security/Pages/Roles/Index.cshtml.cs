using GSF.Application.Security.Roles.Queries.GetRolesByCurrentGroupOwner;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Common.Session;
using GS.Certifications.Web.Pages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Roles;

public class IndexRoleModel : BasePageModel
{

    public IndexRoleModel(IStringLocalizer<Shared> loc, IParametersSessionStoreService parametersSessionStoreService)
    {
        Title = loc["Roles"];
        _loc = loc;
        _parametersSessionStoreService = parametersSessionStoreService;
    }

    private readonly IParametersSessionStoreService _parametersSessionStoreService;
    private readonly IStringLocalizer<Shared> _loc;

    [BindProperty] public List<RoleListDto> Roles { get; private set; }

    [Display(Name = "Nombre")]
    [BindProperty(SupportsGet = true)] public string SearchName { get; set; }
    public bool IsPostBack { get; set; } = false;
    public string NoDataMessage { get; private set; }

    private void SetNoDataMessage()
    {
        NoDataMessage = IsPostBack ? _loc["No hay datos"] : _loc["Click en 'Buscar' para traer resultados"];
    }
    public async Task OnGetAsync()
    {
        await GetParametersFromSession();
        SetNoDataMessage();
        await FilterRoles();
    }

    public async Task OnGetSearch()
    {
        IsPostBack = true;
        SetNoDataMessage();
        await SaveParametersInSession();
        await FilterRoles();
    }

    private async Task GetParametersFromSession()
    {
        dynamic parameters = await _parametersSessionStoreService.GetParametersAsync("roles");

        if (parameters != null)
        {
            SearchName = parameters.SearchName;
        }
    }

    private async Task SaveParametersInSession()
    {
        dynamic par = new
        {
            SearchName,
        };

        string key = "roles";

        await _parametersSessionStoreService.SaveParametersAsync(par, key);
    }

    private async Task FilterRoles()
    {
        var query = new GetRolesByCurrentGroupIdFilteredQuery() { Name = SearchName };
        Roles = await Mediator.Send(query);
    }
}