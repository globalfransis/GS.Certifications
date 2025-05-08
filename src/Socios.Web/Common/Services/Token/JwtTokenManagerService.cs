using GS.Certifications.Application.Interfaces;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Token;
using GSF.Application.Security.Users.Commands.PasswordChange;
using GSF.Application.Security.Users.Commands.UserActivation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Socios.Web.Common.Services.Token;
public class JwtTokenManagerService : ITokenManagerService, IHandshakeTokenService
{
    private readonly ITargetPathResolver _targetPathResolver;
    private readonly TokenEncryptor _tokenEncryptor;
    private readonly TokenConfigurationBuilder _tokenConfigurationBuilder;

    private const string passwordRecoveryTokenType = "passwordRecovery";
    private const string userActivationTokenType = "userActivation";

    public JwtTokenManagerService(ITargetPathResolver targetPathResolver,
                                  TokenEncryptor tokenEncryptor,
                                  TokenConfigurationBuilder tokenConfigurationBuilder)
    {
        _targetPathResolver = targetPathResolver;
        _tokenEncryptor = tokenEncryptor;
        _tokenConfigurationBuilder = tokenConfigurationBuilder;
    }

    private string GenerateGenericToken(string userId, string domainFIdm, string secretKey, string tokenType, TokenBasicConfiguration tokenConfiguration)
    {
        var signinKey = tokenConfiguration.SigninKey;
        var durationInMinutes = tokenConfiguration.Duration;
        var audienceIdentity = tokenConfiguration.AudienceIdentity;

        var keyBytes = Encoding.ASCII.GetBytes(signinKey);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                            new Claim[] {
                                new Claim("sub", userId),
                                new Claim("domain", domainFIdm),
                                new Claim("secretKey", secretKey),
                                new Claim("tokenType", tokenType)
                            }),
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(durationInMinutes),
            Audience = audienceIdentity,
            Issuer = _targetPathResolver.GetHost(),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                                                        SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        stringToken = _tokenEncryptor.EncryptToken(stringToken);
        return stringToken;
    }

    public string GenerateTokenForPasswordRecovery(string userId, string domainFIdm, string secretKey)
    {
        var passwordRecoveryTokenConfiguration = _tokenConfigurationBuilder.GetTokenConfiguration(TokenConfigurationBuilder._passwordRecoveryTokenConfig);
        return GenerateGenericToken(userId, domainFIdm, secretKey, passwordRecoveryTokenType, passwordRecoveryTokenConfiguration);
    }

    public string GenerateTokenForUserActivation(string userId, string domainFIdm, string secretKey)
    {
        var userActivationTokenConfiguration = _tokenConfigurationBuilder.GetTokenConfiguration(TokenConfigurationBuilder._userActivationTokenConfig);
        return GenerateGenericToken(userId, domainFIdm, secretKey, userActivationTokenType, userActivationTokenConfiguration);
    }

    private (string UserId, string DomainFIdm, string SecretKey) RetrieveGenericTokenData(string token, string tokenTypeValue, TokenBasicConfiguration tokenConfiguration)
    {
        string userId = "", domainFidm = "", secretKey = "", tokenType = "";

        var signinKey = tokenConfiguration.SigninKey;
        var audienceIdentity = tokenConfiguration.AudienceIdentity;
        var keyBytes = Encoding.ASCII.GetBytes(signinKey);
        JwtBearerOptions jwtOpt = new()
        {
            SaveToken = true,
            TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = audienceIdentity,
                ValidIssuer = _targetPathResolver.GetHost(),
            }
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            token = _tokenEncryptor.DecryptToken(token);
            //This also validate the token expiration value.
            tokenHandler.ValidateToken(token, jwtOpt.TokenValidationParameters, out SecurityToken resultadoToken);

            var claimData = tokenHandler.ReadJwtToken(token).Claims;
            userId = claimData.FirstOrDefault(x => x.Type == "sub").Value;
            domainFidm = claimData.FirstOrDefault(x => x.Type == "domain").Value;
            secretKey = claimData.FirstOrDefault(x => x.Type == "secretKey").Value;
            tokenType = claimData.FirstOrDefault(x => x.Type == "tokenType").Value;

            if (tokenType != tokenTypeValue) throw GenerateTokenException(tokenTypeValue, token, userId, domainFidm);

        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case SecurityTokenException:
                    //throw
                    throw GenerateTokenException(tokenTypeValue, token, userId, domainFidm);
                default:
                    //throw new SecurityTokenException(ex.Message, ex);
                    throw GenerateTokenException(tokenTypeValue, token, userId, domainFidm);
            }
        }

        return (userId, domainFidm, secretKey);
    }

    private Exception GenerateTokenException(string tokenType, string token, string userId, string domainFidm)
    {
        switch (tokenType)
        {
            case passwordRecoveryTokenType:
                Log.Information("Tipo de Token incorrecto: {@token} - {@tokenType}", token, tokenType);
                return new InvalidPasswordChangeTokenException() { UserId = userId, DomainFIdm = domainFidm, Token = token };
            case userActivationTokenType:
                Log.Information("Tipo de Token incorrecto: {@token} - {@tokenType}", token, tokenType);
                return new InvalidUserActivationTokenException() { UserId = userId, DomainFIdm = domainFidm, Token = token };


            default:
                Log.Information("No existe el token type: {tokenType}", tokenType);
                return new InvalidPasswordChangeTokenException() { UserId = userId, DomainFIdm = domainFidm, Token = token };
        }
    }

    public (string UserId, string DomainFIdm, string SecretKey) RetrieveTokenDataForPasswordRecovery(string token)
    {
        var passwordRecoveryTokenConfiguration = _tokenConfigurationBuilder.GetTokenConfiguration(TokenConfigurationBuilder._passwordRecoveryTokenConfig);
        return RetrieveGenericTokenData(token, passwordRecoveryTokenType, passwordRecoveryTokenConfiguration);
    }

    public (string UserId, string DomainFIdm, string SecretKey) RetrieveTokenDataForUserActivation(string token)
    {
        var userActivationTokenConfiguration = _tokenConfigurationBuilder.GetTokenConfiguration(TokenConfigurationBuilder._userActivationTokenConfig);
        return RetrieveGenericTokenData(token, userActivationTokenType, userActivationTokenConfiguration);
    }

    public string GetHandshakeToken(string sub, string secretKey, string domainFIdm)
    {
        throw new NotImplementedException();
    }

    public (string sub, string DomainFIdm, string SecretKey) RetrieveHandshakeTokenData(string token)
    {
        throw new NotImplementedException();
    }
}
