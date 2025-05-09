using GSF.Application.Security.Token;
using Microsoft.Extensions.Configuration;

namespace GS.Certifications.Web.Common.Services.Token;

public class TokenConfigurationBuilder
{
    private readonly IConfiguration _configuration;
    public const string _passwordRecoveryTokenConfig = "_passwordRecoveryTokenConfig";
    public const string _userActivationTokenConfig = "_userActivationTokenConfig";

    public TokenConfigurationBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenBasicConfiguration GetTokenConfiguration(string type)
    {
        TokenBasicConfiguration tokenBasicConfiguration = null;
        switch (type)
        {
            case _passwordRecoveryTokenConfig:
                tokenBasicConfiguration = new PasswordRecoveryTokenConfiguration();
                _configuration.GetSection("TokenConfig:PasswordRecoveryTokenConfig").Bind(tokenBasicConfiguration, c => c.BindNonPublicProperties = true);
                break;

            case _userActivationTokenConfig:
                tokenBasicConfiguration = new UserActivationTokenConfiguration();
                _configuration.GetSection("TokenConfig:UserActivationTokenConfig").Bind(tokenBasicConfiguration, c => c.BindNonPublicProperties = true);
                break;

            default:
                break;
        }

        return tokenBasicConfiguration;
    }
}
