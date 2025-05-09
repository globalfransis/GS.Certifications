using GSF.Application.Security.Token;
using Microsoft.Extensions.Configuration;

namespace GS.Certifications.Web.Common.Services.Token;

public class WebTokenEncryptorConfiguration : TokenEncryptorConfiguration
{
    public WebTokenEncryptorConfiguration(IConfiguration configuration)
    {
        EncriptionKey = configuration.GetSection("TokenEncryptorToken").Value;
    }
}
