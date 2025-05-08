using GSF.Application.Security.Token;

namespace Socios.Web.Common.Services.Token;

public class WebTokenEncryptorConfiguration : TokenEncryptorConfiguration
{
    public WebTokenEncryptorConfiguration(IConfiguration configuration)
    {
        EncriptionKey = configuration.GetSection("TokenEncryptorToken").Value;
    }
}
