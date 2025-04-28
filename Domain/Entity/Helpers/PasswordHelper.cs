using System.Security.Cryptography;
using System.Text;

namespace Entity.Helpers;

public class PasswordHelper
{
    private const int KeySize = 32;
    private const int IterationsCount = 1000;

    public static string Encrypt(string password, string salt)
    {
        using var algorithm = new Rfc2898DeriveBytes(
            password: password,
            salt: Encoding.UTF8.GetBytes(salt),
            iterations: IterationsCount,
            hashAlgorithm: HashAlgorithmName.SHA256);
        return Convert.ToBase64String(algorithm.GetBytes(KeySize));
    }

    public static bool Verify(string passwordHash, string password, string salt)
    {
        return Encrypt(password, salt).SequenceEqual(passwordHash);
    }
}