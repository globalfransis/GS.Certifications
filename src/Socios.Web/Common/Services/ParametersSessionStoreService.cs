using AutoMapper;
using GSF.Application.Common.Interfaces;
using Socios.Web.Common.Session;

namespace Socios.Web.Common.Services;

public class ParametersSessionStoreService : IParametersSessionStoreService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly ITargetPathResolver _targetPathResolver;

    public ParametersSessionStoreService(IHttpContextAccessor httpContextAccessor,
                                       IMapper mapper, ITargetPathResolver targetPathResolver)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _targetPathResolver = targetPathResolver;

    }

    public Task SaveParametersAsync(object parameters, string key)
    {
        //string url = GetUrl();

        ClearValueAsync(key);

        _httpContextAccessor.HttpContext.Session.SetComplexData(key, parameters);

        return Task.CompletedTask;
    }

    private string GetUrl()
    {
        string path = _targetPathResolver.GetResolvedUri(string.Empty);
        string host = _targetPathResolver.GetHost();

        return host + path;
    }

    public Task<object> GetParametersAsync(string key)
    {
        //string url = GetUrl();

        object parameters = _httpContextAccessor.HttpContext.Session.GetComplexData<object>(key);

        return Task.FromResult(parameters);
    }

    public Task ClearValueAsync(string key)
    {
        _httpContextAccessor.HttpContext.Session.Remove(key);

        return Task.CompletedTask;
    }

}
