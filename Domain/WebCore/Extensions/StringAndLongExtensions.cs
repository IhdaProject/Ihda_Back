using Entity.Exceptions;
using Entity.Helpers;
using WebCore.Constants;

namespace WebCore.Extensions;

public static class StringAndLongExtensions
{
    public static int AddMinutes { get; set; } = 60;
    public static string EncryptId(this long id, long userId)
    {
        return EncryptionHelper.Encrypt(id, userId, StaticCache.SymmetricKey);
    }
    public static string EncryptUrl(this string url, long userId)
    {
        return EncryptionHelper.EncryptString(url, userId, StaticCache.SymmetricKey);
    }

    public static long DecryptId(this string encryptedData, long currentUserId)
    {
        try
        {
            var decrypt = EncryptionHelper.Decrypt(encryptedData, StaticCache.SymmetricKey);
            if (decrypt.Id is 0 || decrypt.UserId != currentUserId || decrypt.Timestamp.AddMinutes(AddMinutes) < DateTime.Now)
                throw new AlreadyExistsException("Forbidden");
        
            return decrypt.Id;
        }
        catch (Exception e)
        {
            throw new AlreadyExistsException("Forbidden");
        }
    }
    public static string DecryptUrl(this string encryptedData, long currentUserId)
    {
        try
        {
            var decrypt = EncryptionHelper.DecryptString(encryptedData, StaticCache.SymmetricKey);
            if (decrypt.url is null || decrypt.UserId != currentUserId)
                throw new AlreadyExistsException("Forbidden");
        
            return decrypt.url;
        }
        catch (Exception e)
        {
            throw new AlreadyExistsException("Forbidden");
        }
    }
    
    public static (long id, string str) DecryptString(this string encryptedData)
    {
        try
        {
            var decrypt = EncryptionHelper.DecryptString(encryptedData, StaticCache.SymmetricKey);
            return (decrypt.UserId, decrypt.url);
        }
        catch (Exception e)
        {
            throw new AlreadyExistsException("Forbidden");
        }
    }
}