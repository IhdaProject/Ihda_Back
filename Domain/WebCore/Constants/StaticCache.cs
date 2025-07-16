namespace WebCore.Constants;

public static class StaticCache
{
    public static Dictionary<long, List<int>> Permissions { get; set; } = new();
    public static string SymmetricKey { get; set; }
}