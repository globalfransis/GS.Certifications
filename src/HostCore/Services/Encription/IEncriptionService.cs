namespace HostCore.Services.Encription;

public interface IEncriptionService
{
    public string Encrypt(string plainText, string secretKey);
    public string Encrypt(object objectData, string secretKey);
    public string Decrypt(string cipherText, string secretKey);
    public T? Decrypt<T>(string cipherText, string secretKey);
}
