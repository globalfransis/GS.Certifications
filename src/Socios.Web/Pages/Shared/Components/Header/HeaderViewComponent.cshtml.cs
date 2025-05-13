using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Domain.Entities.Empresas;
using GSF.Application.Common.Interfaces;
using GSF.Domain.Entities.Companies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Common.Services.Empresa;

namespace Socios.Web.Pages.Shared.Components.Header;

public class HeaderViewComponent : ViewComponent
{
    public readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IStringLocalizer<Common.Resources.Shared> _loc;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICurrentSocioService _currentEmpresaPortalService;
    private readonly IUsuarioEmpresaPortalService _usuarioEmpresaPortalService;

    public string CurrentEnvironment { get; set; }
    public string UserName { get; set; }
    public Company CurrentCompany { get; set; }
    public EmpresaPortal CurrentEmpresaPortal { get; set; }



    public HeaderViewComponent(
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IStringLocalizer<Common.Resources.Shared> sharedLocalizer,
        ICurrentUserService currentUserService,
        ICurrentSocioService currentEmpresaPortalService,
        IUsuarioEmpresaPortalService usuarioEmpresaPortalService
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _loc = sharedLocalizer;
        _currentUserService = currentUserService;
        _currentEmpresaPortalService = currentEmpresaPortalService;
        _usuarioEmpresaPortalService = usuarioEmpresaPortalService;


    }

    public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
    {
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            UserName = _currentUserService.UserLogin;
            long? uepId = _currentEmpresaPortalService.GetCurrentUsuarioEmpresaPortalId();
            var uep = await _usuarioEmpresaPortalService.GetByIdAsync((long)uepId);
            CurrentEmpresaPortal = uep.EmpresaPortal;
            CurrentCompany = CurrentEmpresaPortal.Company;
        }

        CurrentEnvironment = _hostEnvironment.IsProduction() ? "Produccion" : "Demo";

        return await Task.FromResult(View("HeaderViewComponent", this));
    }

}
