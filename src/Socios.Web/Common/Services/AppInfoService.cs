using GSF.Application.Common.Interfaces;

namespace Socios.Web.Common.Services;

public class AppInfoService : IAppInfoService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContext;

    public AppInfoService(IWebHostEnvironment environment, IHttpContextAccessor httpContext)
    {
        _environment = environment;
        _httpContext = httpContext;
    }

    public string CompleteFileSystemPath(string path) => Path.Combine(_environment.WebRootPath, path);
}