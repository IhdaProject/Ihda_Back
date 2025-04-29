using System.Security.Cryptography;
using System.Text;

namespace Entity.Helpers;

public static class EncryptionHelper
{
    public static string Encrypt(long id, long userId, string key)
    {
        var timestamp = DateTime.UtcNow.Ticks;
        var data = $"{id}:{userId}:{timestamp}";
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)[..32]);
        aesAlg.IV = GenerateRandomBytes(aesAlg.BlockSize / 8);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(data);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }
    public static string EncryptString(string url, long userId, string key)
    {
        var data = $"{url}:{userId}";
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)[..32]);
        aesAlg.IV = GenerateDeterministicIv(url, userId, key, aesAlg.BlockSize / 8);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(data);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }
    public static string EncryptStringWithTime(string token, long id, string key, DateTime? expire = null)
    {
        var timestamp = (expire ?? DateTime.UtcNow).Ticks;
        var data = $"{id}:{token}:{timestamp}";
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)[..32]);
        aesAlg.IV = GenerateRandomBytes(aesAlg.BlockSize / 8);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(data);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }
    public static (int Id, int UserId, DateTime Timestamp) Decrypt(string encrypted, string key)
    {
        var fullCipher = Convert.FromBase64String(encrypted);
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)[..32]);
        var iv = new byte[aesAlg.BlockSize / 8];
        var cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        var decrypted = srDecrypt.ReadToEnd();

        var parts = decrypted.Split(':');
        if (parts.Length != 3) return default;

        var id = int.Parse(parts[0]);
        var userId = int.Parse(parts[1]);
        var timestamp = new DateTime(long.Parse(parts[2]));

        return (id, userId, timestamp);
    }
    public static (string url, int UserId) DecryptString(string encrypted, string key)
    {
        var fullCipher = Convert.FromBase64String(encrypted);
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)[..32]);
        var iv = new byte[aesAlg.BlockSize / 8];
        var cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        var decrypted = srDecrypt.ReadToEnd();

        var parts = decrypted.Split(':');
        if (parts.Length != 2) return default;

        var url = parts[0];
        var userId = int.Parse(parts[1]);

        return (url, userId);
    }
    public static (int Id, string Token, DateTime Timestamp) DecryptStringWithTime(string encrypted, string key)
    {
        var fullCipher = Convert.FromBase64String(encrypted);
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)[..32]);
        var iv = new byte[aesAlg.BlockSize / 8];
        var cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        var decrypted = srDecrypt.ReadToEnd();

        var parts = decrypted.Split(':');
        if (parts.Length != 3) return default;

        var id = int.Parse(parts[0]);
        var token = parts[1];
        var timestamp = new DateTime(long.Parse(parts[2]));

        return (id, token, timestamp);
    }
    private static byte[] GenerateRandomBytes(int length)
    {
        var random = new RNGCryptoServiceProvider();
        var bytes = new byte[length];
        random.GetBytes(bytes);
        return bytes;
    }
    private static byte[] GenerateDeterministicIv(string url, long userId, string key, int ivSize)
    {
        var input = $"{url}:{userId}:{key}";
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return hash[..ivSize];
    }
}