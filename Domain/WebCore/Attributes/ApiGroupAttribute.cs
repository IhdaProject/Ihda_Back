namespace WebCore.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ApiGroupAttribute(params string[] groupNames) : Attribute
{
    public string[] GroupNames { get; } = groupNames;
}