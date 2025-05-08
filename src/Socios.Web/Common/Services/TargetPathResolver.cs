using GSF.Application.Common.Interfaces;

namespace Socios.Web.Common.Services;

public class TargetPathResolver : ITargetPathResolver
{

    private readonly HttpContext _httpContext;

    public TargetPathResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public string GetScheme()
    {
        return _httpContext.Request.Scheme;
    }

    public string GetResolvedUri(string path)
    {
        string scheme = _httpContext.Request.Scheme;
        string host = _httpContext.Request.Host.Value.Trim('/');
        string virtualDirectory = _httpContext.Request.PathBase.ToString().Trim('/');
        if (!string.IsNullOrEmpty(virtualDirectory))
        {
            virtualDirectory = $"/{virtualDirectory}";
        }
        path = path.Trim('/');
        //Serilog.Log.Information("Path:{path} Scheme: {Scheme}, Host: {host}, virtualDirectory: {virtualDirectory}, Result: {result}", path, scheme, host, virtualDirectory, $@"{scheme}://{host}{virtualDirectory}/{path}");
        return $@"{virtualDirectory}/{path}";
    }
    public string GetHost()
    {
        return _httpContext.Request.Host.Value.Trim('/');
    }

}
