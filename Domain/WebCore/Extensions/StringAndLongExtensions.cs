using Entity.Exceptions;
using Entity.Helpers;
using WebCore.Constants;

namespace WebCore.Extensions;

public static class StringAndLongExtensions
{
    private static int AddMinutes => 60;

    public static string EncryptId(this long id, string type, long userId)
    {
        return EncryptionHelper.Encrypt(type, id, userId, StaticCache.SymmetricKey);
    }
    public static List<string> EncryptId(this List<long> ids, string type, long userId)
    {
        return [..ids.Select(id => EncryptionHelper.Encrypt(type, id, userId, StaticCache.SymmetricKey))];
    }
    public static string EncryptUrl(this string url, long userId)
    {
        return EncryptionHelper.EncryptString(url, userId, StaticCache.SymmetricKey);
    }

    public static long DecryptId(this string encryptedInfo, string type, long currentUserId)
    {
        try
        {
            var decrypt = EncryptionHelper.Decrypt(encryptedInfo, StaticCache.SymmetricKey);
            if (decrypt.Type is null or {Length:0} ||
                decrypt.Type != type || decrypt.Id is 0 ||
                decrypt.UserId != currentUserId ||
                decrypt.Timestamp.AddMinutes(AddMinutes) < DateTime.UtcNow)
                throw new AlreadyExistsException("Forbidden");
        
            return decrypt.Id;
        }
        catch (Exception e)
        {
            throw new AlreadyExistsException("Forbidden");
        }
    }
    public static List<long> DecryptId(this List<string> encryptedData, string type, long currentUserId)
    {
        return
        [
            ..encryptedData.Select(encryptedInfo =>
            {
                try
                {
                    var decrypt = EncryptionHelper.Decrypt(encryptedInfo, StaticCache.SymmetricKey);
                    if (decrypt.Type is null or { Length: 0 } ||
                        decrypt.Type != type || decrypt.Id is 0 ||
                        decrypt.UserId != currentUserId ||
                        decrypt.Timestamp.AddMinutes(AddMinutes) < DateTime.UtcNow)
                        throw new AlreadyExistsException("Forbidden");

                    return decrypt.Id;
                }
                catch (Exception e)
                {
                    throw new AlreadyExistsException("Forbidden");
                }
            })
        ];
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