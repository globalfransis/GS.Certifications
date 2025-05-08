using GS.Certifications.Application.Commons.Dtos.Sistemas;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using HostCore.Dtos.Session;
using HostCore.Services.Encription;
using Microsoft.AspNetCore.Authorization;
using Socios.Web.Common.Services.Empresa;
using Socios.Web.Common.Services.Handshake;
using Socios.Web.Common.Services.Modules;
using System.Web;

namespace Socios.Web.Pages;

[Authorize]
public class PortalModel : BasePageModel
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICurrentCompanyService _currentCompanyService;
    private readonly ICurrentEmpresaPortalService _currentEmpresaPortalService;
    private readonly IModulesService _modulesService;
    private readonly IHandshakeService _handshakeService;
    private readonly IEncriptionService _encriptionService;
    public string ModuleLandingUrl { get; set; }
    public PortalModel(
        ICurrentUserService currentUserService,
        ICurrentCompanyService currentCompanyService,
        ICurrentEmpresaPortalService currentEmpresaPortalService,
        IModulesService modulesService,
        IHandshakeService handshakeService,
        IEncriptionService encriptionService)
    {
        _currentUserService = currentUserService;
        _currentCompanyService = currentCompanyService;
        _currentEmpresaPortalService = currentEmpresaPortalService;
        _modulesService = modulesService;
        _handshakeService = handshakeService;
        _encriptionService = encriptionService;
    }

    public async Task OnGet()
    {
        var userId = _currentUserService.UserId;
        var companyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();
        var empresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var roleId = _currentEmpresaPortalService.GetCurrentRolTipoIdm();
        var module = _modulesService.GetByRole((int)roleId);

        HandshakeResponseDto handshakeResponseDto = await _handshakeService.ExecuteAsync(module);

        SessionDataDto sessionDataDto = new() { UserId = userId, CompanyId = companyId, EmpresaPortalId = (int)empresaPortalId, RoleId = (long)roleId };
        string data = _encriptionService.Encrypt(sessionDataDto, handshakeResponseDto.EncriptionKey);

        string encodedData = HttpUtility.UrlEncode(data);
        string encodedToken = HttpUtility.UrlEncode(handshakeResponseDto.Token);

        ModuleLandingUrl = $"{module.LandingUrl}?data={encodedData}&token={encodedToken}";
    }
}
