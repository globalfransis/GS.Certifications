using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace HostCore.Services.Encription;

public class AesEncryptionService : IEncriptionService
{
    private byte[] _key; // Clave secreta
    private byte[] _iv;  // Vector de inicialización (IV)

    private void SetKeys(string secretKey)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
        }

        using (MD5 md5 = MD5.Create())
        {
            _iv = md5.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
        }
    }

    public string Encrypt(string plainText, string secretKey)
    {
        SetKeys(secretKey);

        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using (var writer = new StreamWriter(cryptoStream))
                {
                    writer.Write(plainText);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string Encrypt(object objectData, string secretKey)
    {
        string json = JsonConvert.SerializeObject(objectData);
        return Encrypt(json, secretKey);
    }

    public string Decrypt(string cipherText, string secretKey)
    {
        SetKeys(secretKey);

        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream(buffer))
            using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
    public T? Decrypt<T>(string cipherText, string secretKey)
    {
        string json = Decrypt(cipherText, secretKey);
        return JsonConvert.DeserializeObject<T>(json);
    }
}



