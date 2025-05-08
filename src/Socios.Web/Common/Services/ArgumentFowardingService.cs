using System.Collections.Specialized;
using System.Web;

namespace Socios.Web.Common.Services;

public class ArgumentFowardingService : IArgumentFowardingService
{
    readonly HttpContext _httpContext;

    public ArgumentFowardingService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public string SetForwardedArguments(string targetRelativeUrl)
    {
        string resultUrl = targetRelativeUrl;

        NameValueCollection currentArgs = HttpUtility.ParseQueryString(_httpContext.Request.QueryString.Value);

        Uri TargetAbsoluteUri = new Uri(new Uri(_httpContext.Request.Host.ToString()), targetRelativeUrl);
        string decodedQuery = HttpUtility.UrlDecode(TargetAbsoluteUri.Query);
        NameValueCollection targetArgs = HttpUtility.ParseQueryString(decodedQuery);

        foreach (string targetArg in targetArgs)
        {
            string argument = GetEscapedArgument(targetArgs[targetArg]);

            if (argument != null)
            {
                foreach (string currentArg in currentArgs)
                {
                    if (currentArg.Equals(argument.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        resultUrl = resultUrl.Replace(targetArgs[targetArg], currentArgs[currentArg]);
                    }
                }

            }
        }

        return resultUrl;

    }

    private string GetEscapedArgument(string value)
    {
        // si es un argumento encerrado entre llaves
        if (value.StartsWith("{") && value.EndsWith("}"))
        {
            return value[1..^1];
        }
        else
            return null;
    }
}
