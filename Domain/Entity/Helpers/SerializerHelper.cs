using System.Text.Json;

namespace Entity.Helpers;

public static class SerializerHelper
{
    public static string ToJsonString(object data)
    {
        return JsonSerializer.Serialize(data);
    }

    public static T? FromJsonString<T>(string content)
    {
        return JsonSerializer.Deserialize<T>(content);
    }
}