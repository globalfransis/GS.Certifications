using GSF.Application.Security.SecurityTemp;
using GSF.Application.Security.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Pages.Shared.Components.Header;

public class HeaderViewComponent : ViewComponent
{
    public readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<Common.Resources.Shared> _loc;
    private readonly ISecurityTempStoreService _securityTempStoreService;

    public string UserName { get; set; }
    public string UserGroup { get; set; }
    public List<SecurityUserCompaniesDto> UserCompanies { get; set; } = new List<SecurityUserCompaniesDto>();
    public SecurityUserCompaniesDto CurrentCompany { get; set; }


    public HeaderViewComponent(ISecurityTempStoreService securityTempStoreService,
                               IHttpContextAccessor httpContextAccessor,
                               IStringLocalizer<Common.Resources.Shared> sharedLocalizer)
    {
        _securityTempStoreService = securityTempStoreService;
        _httpContextAccessor = httpContextAccessor;
        _loc = sharedLocalizer;
    }

    public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
    {
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            UserName = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserGroup = (await _securityTempStoreService.GetCurrentUserCompanyAsync())?.Group.Name;
            UserCompanies = await _securityTempStoreService.GetUserCompaniesAsync() ?? new List<SecurityUserCompaniesDto>();
            CurrentCompany = await _securityTempStoreService.GetCurrentUserCompanyAsync();
        }

        return await Task.FromResult(View("HeaderViewComponent", this));
    }

}
