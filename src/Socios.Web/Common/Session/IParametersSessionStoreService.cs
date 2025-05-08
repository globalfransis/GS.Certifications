namespace Socios.Web.Common.Session;

public interface IParametersSessionStoreService
{
    Task SaveParametersAsync(object parameters, string key);

    Task<object> GetParametersAsync(string key);
}
