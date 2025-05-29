using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Domain.Entities.Empresas;
using GSF.Application.Security.Options.GetOptions.Queries;
using GSF.Application.Security.Options.Queries;
using GSF.Domain.Entities.Companies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Common.Services;
using Socios.Web.Common.Services.Empresa;
using Socios.Web.Common.Services.Empresa.Storage;
using Socios.Web.Common.Services.Modules;

namespace Socios.Web.Pages;

[Authorize]
public class IndexModel : BasePageModel
{
    private readonly IStringLocalizer<Common.Resources.Shared> _loc;
    private readonly ICertificationsDbContext _dbContext;
    private readonly IUsuarioEmpresaPortalService _usuarioEmpresaPortalService;
    private readonly ICurrentSocioService _currentEmpresaPortalService;
    private readonly ICurrentEmpresaPortalRepository _currentEmpresaPortalRepository;
    private readonly IModulesService _modulesService;

    public Company CurrentCompany { get; set; }
    public EmpresaPortal CurrentEmpresaPortal { get; set; }
    public List<Module> Modules { get; set; }


    public OptionDto Options { get; set; }
    public OptionDto ContextOptions { get; set; }

    public string HtmlChangeLog { get; set; }
    public bool IsMiFroActive { get; set; }
    public string CurrentPage { get; set; }
    public IHttpContextAccessor _httpContextAccessor { get; }
    private readonly IArgumentFowardingService _argumentFowardingService;


    public IndexModel(
        IStringLocalizer<Common.Resources.Shared> sharedLocalizer,
        ICertificationsDbContext dbContext,
        IUsuarioEmpresaPortalService usuarioEmpresaPortalService,
        ICurrentSocioService currentEmpresaPortalService,
        ICurrentEmpresaPortalRepository currentEmpresaPortalRepository,
                IModulesService modulesService,
        IHttpContextAccessor httpContextAccessor, IArgumentFowardingService argumentFowardingService)
    {
        _loc = sharedLocalizer;
        _dbContext = dbContext;
        _usuarioEmpresaPortalService = usuarioEmpresaPortalService;
        _currentEmpresaPortalService = currentEmpresaPortalService;
        _currentEmpresaPortalRepository = currentEmpresaPortalRepository;
        _modulesService = modulesService;
        Title = "Inicio";
        ShowTitle = false;
        _httpContextAccessor = httpContextAccessor;
        _argumentFowardingService = argumentFowardingService;
    }

    public async Task OnGet()
    {
        IsMiFroActive = _modulesService.IsActive();
        long? cuepId = _currentEmpresaPortalService.GetCurrentUsuarioEmpresaPortalId();

        if (cuepId is not null)
        {
            var uep = await _usuarioEmpresaPortalService.GetByIdAsync((long)cuepId);
            CurrentEmpresaPortal = uep.EmpresaPortal;
            CurrentCompany = CurrentEmpresaPortal.Company;

            if (uep is not null && uep.Habilitado)
            {
                List<Module> modules = _modulesService.GetAll(true);
                Modules = modules.Where(m => uep.Roles.Any(uepr => uepr.Habilitado && uepr.RolTipoId == m.RoleId && uep.EmpresaPortal.Roles.Any(er => er.RolTipo == uepr.RolTipo))).ToList();
            }
        }

        CurrentPage = _httpContextAccessor.HttpContext.Items["navigationKey"].ToString();

        IMediator _mediator = (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));


        var queryContextOptions = new GetContextOptionsByPageQuery
        {
            Page = CurrentPage
        };
        OptionDto rootContextOptions = await _mediator.Send(queryContextOptions);
        foreach (var option in rootContextOptions.Options)
        {
            SetForwardedArguments(option);
        }
        ContextOptions = rootContextOptions;

        var queryOptions = new GetOptionsTreeQuery();
        OptionDto options = await _mediator.Send(queryOptions);

        Options = options;

        GetStringTranslations();
    }

    public IActionResult OnPostProcessRole(long? rolIdm)
    {
        RolTipo rol = _dbContext.RolesTipos.FirstOrDefault(r => r.Idm == rolIdm);
        _currentEmpresaPortalRepository.SetRolTipo(rol);

        return RedirectToPage("/Portal");
    }

    private void SetForwardedArguments(OptionDto option)
    {
        option.Url = _argumentFowardingService.SetForwardedArguments(option.Url);
        foreach (var childOption in option.Options)
        {
            SetForwardedArguments(childOption);
        }
    }
}
