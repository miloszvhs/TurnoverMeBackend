using System.Security.Cryptography;

namespace FrontTurnoverMe.Application.Services;

public interface IAesService
{
    public string DecryptBase64String(string encryptedString);
}

public class AesService : IAesService
{
    private readonly string key;
    private readonly string iv;

    public AesService()
    {
        key = "/+uprDkEHWAcBEYVtE45E7FuIQVW8IqWyd1D3obY4lE=";
        iv = "A4T7L4RC17uit4GCv5LUrw==";
    }
    
    public string DecryptBase64String(string encryptedString)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = Convert.FromBase64String(key);
        aesAlg.IV = Convert.FromBase64String(iv);

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedString));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        
        var plaintext = srDecrypt.ReadToEnd();
        return plaintext;
    }
}