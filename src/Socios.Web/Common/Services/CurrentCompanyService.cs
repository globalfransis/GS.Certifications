using GSF.Application.Security.SecurityTemp;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;

namespace Socios.Web.Common.Services;

public class CurrentCompanyService : ICurrentCompanyService
{
    private long? _companyId = null;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISecurityTempStoreService _securityTempStoreService;

    public CurrentCompanyService(IHttpContextAccessor httpContextAccessor, ISecurityTempStoreService securityTempStoreService)
    {
        _httpContextAccessor = httpContextAccessor;
        _securityTempStoreService = securityTempStoreService;
    }

    public void SetCookieCurrentCompany(SecurityUserCompaniesDto company)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Append(
        "boarding.companyId",
        company.Id.ToString(),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
    }

    public async Task<long> GetCurrentCompanyIdLazyAsync()
    {
        if (!_companyId.HasValue)
            _companyId = (await GetCurrentCompanyAsync()).Id;
        return _companyId.Value;
    }

    public async Task<SecurityUserCompaniesDto> GetCurrentCompanyAsync()
    {
        var securityCompanyDto = await _securityTempStoreService.GetCurrentUserCompanyAsync();
        return securityCompanyDto;
    }

    public async Task<SecurityGroupDto> GetCurrentCompanyGroupAsync()
    {
        var securityGroupDto = (await _securityTempStoreService.GetCurrentUserCompanyAsync())?.Group;
        return securityGroupDto;
    }

    public async Task<SecurityOrganizationDto> GetCurrentCompanyOrganizationAsync()
    {
        var securityOrganizationDto = (await _securityTempStoreService.GetCurrentUserCompanyAsync())?.Organization;
        return securityOrganizationDto;
    }
}