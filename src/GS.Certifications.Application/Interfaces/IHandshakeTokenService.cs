namespace GS.Certifications.Application.Interfaces;

public interface IHandshakeTokenService
{
    string GetHandshakeToken(string sub, string secretKey, string domainFIdm);

    (string sub, string DomainFIdm, string SecretKey) RetrieveHandshakeTokenData(string token);
}
