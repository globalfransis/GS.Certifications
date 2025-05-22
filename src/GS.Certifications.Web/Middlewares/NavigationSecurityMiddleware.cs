using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Options.Queries;
using GSF.Application.Security.SecurityTemp;
using GSFSharedResources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Middlewares;

/// </summary>
//[DebuggerStepThrough]
public class NavigationSecurityMiddleware
{
    private readonly RequestDelegate _next;
    private const string _modeIndex = "index";
    private const string _modeDetails = "detail";
    private const string _modeCreate = "create";
    private const string _modeEdit = "edit";
    private const string _modDdelete = "delete";

    private const string _grantUpdate = "update";

    // TODO: ver por que no se puede navegar a las rutas de las options incluso teniendo permisos
    private readonly ReadOnlyCollection<string> WhiteListPages = new ReadOnlyCollection<string>(new[] { "/security/login", "/security/passwordchange", "/security/passwordrecovery", "/security/useractivation", "/socios/certificaciones/index", "/socios/empresas/index" });

    private HttpContext _httpContext;
    private ISecurityTempStoreService _securityTempStoreService;
    private ILogger<NavigationSecurityMiddleware> _logger;
    private IStringLocalizer<Shared> _loc;
    private ITargetPathResolver _targetPathResolver;

    private readonly string[] _modes = new string[] { _modeIndex, _modeCreate, _modeDetails, _modeEdit, _modDdelete };
    private readonly Dictionary<string, string> _modeRelationship = new Dictionary<string, string>()
    {
        {_modeEdit, _grantUpdate}
    };

    public NavigationSecurityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ISecurityTempStoreService securityTempStoreService,
                             ILogger<NavigationSecurityMiddleware> logger, IStringLocalizer<Shared> loc,
                             ITargetPathResolver targetPathResolver)
    {
        _httpContext = httpContext;
        _securityTempStoreService = securityTempStoreService;
        _logger = logger;
        _loc = loc;
        _targetPathResolver = targetPathResolver;

        bool isAjaxCall = httpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest"
            || httpContext.Request.Path.Value.Contains("/api/");

        bool hasPermission;
        if (isAjaxCall)
            hasPermission = ResolveSecurityForAPICalls();
        else
            hasPermission = await ResolveSecurityForRazorPages();

        if (!hasPermission)
            return; // To avoid hitting await _next.Invoke(httpContext)

        await _next.Invoke(httpContext);
    }

    private bool ResolveSecurityForAPICalls()
    {
        return true;
    }

    private async Task<bool> ResolveSecurityForRazorPages()
    {
        bool hasPermission = true;
        string navigationKey = null;
        var area = _httpContext.Request.RouteValues["area"];
        var page = _httpContext.Request.RouteValues["page"];
        //Every page of the framework (except the home page) should have an area and a page RouteValue.
        //We exclude other pages that dont have these RouteValues like those for signalR (notificationHub, etc)
        //if (area != null && page != null && _httpContext.Request.Path.ToString() != "/Security/Login")

        if (area != null && page != null && !WhiteListPages.Contains(_httpContext.Request.Path.ToString().ToLower()))
        {
            hasPermission = false;
            // One way to obtain the url is httpContext.Request.Path.ToString().ToLower();;
            navigationKey = $"/{area}{page}".ToLower();
            var sections = navigationKey.Split("/");
            var modeRequested = sections[sections.Length - 1].ToLower();

            //By name convention
            navigationKey = navigationKey.Replace(modeRequested, "index");

            //Normalize the Url according toi the virtualPath.
            navigationKey = _targetPathResolver.GetResolvedUri(navigationKey);

            //For Vue pages that use de mode as a QueryString parameter.
            modeRequested = string.IsNullOrEmpty(_httpContext.Request.Query["mode"].ToString()) ? modeRequested : _httpContext.Request.Query["mode"].ToString().ToLower();

            var optionsTree = await _securityTempStoreService.GetOptionsAsync();

            var optionRequested = optionsTree != null ? GetOptionByUrl(optionsTree, navigationKey) : null;

            if (optionRequested != null)
            {
                if (modeRequested is _modeIndex or _modeDetails)
                {
                    hasPermission = true;
                }
                else
                {
                    var grants = await _securityTempStoreService.GetGrantsAsync();
                    var grantSearch = ModifyRequestedMode(modeRequested);
                    var optionGrants = grants.Where(g => g.OptionId == optionRequested.Id).ToList();
                    //hasPermission = optionGrants.Count != 0 && optionGrants.Any(og => og.Name.Contains(grantSearch));
                    hasPermission = optionGrants.Count != 0 && optionGrants.Any(og => og.Name.ToLower().Remove(0, og.Name.LastIndexOf('.') + 1) == grantSearch);
                }
            }

            if (!hasPermission)
            {
                _logger.LogError(_loc["El usuario no tiene permisos para navegar a la página seleccionada. Url buscada: "] + $"/{area}{page}".ToLower() + " modo: " + modeRequested);
                _httpContext.Response.Redirect(_targetPathResolver.GetResolvedUri("/"));
            }
        }

        return hasPermission;
    }

    private string ModifyRequestedMode(string mode)
    {
        var replace = _modeRelationship.GetValueOrDefault(mode);
        return string.IsNullOrEmpty(replace) ? mode : replace;
    }

    private OptionDto GetOptionByUrl(List<OptionDto> optionList, string url)
    {
        OptionDto result = null;
        var i = 0;

        while (i < optionList.Count && result == null)
        {
            var currentOption = optionList[i];
            //if (currentOption.Url.ToLower().Contains(url))
            if (currentOption.Url.ToLower() == url)
                result = currentOption;
            else
                if (currentOption.Options != null && currentOption.Options.Count > 0)
                result = GetOptionByUrl(currentOption.Options, url);

            i++;
        }

        return result;
    }
}

public static class NavigationSecurityMiddlewareExtension
{
    public static IApplicationBuilder UseNavigationSecurityMiddlware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<NavigationSecurityMiddleware>();
    }
}
