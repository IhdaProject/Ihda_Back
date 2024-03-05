using Microsoft.AspNetCore.Mvc;
using WebCore.Filters;

namespace WebCore.Attributes;

public class BasicAuthorizeAttribute : TypeFilterAttribute
{
    BasicAuthorizeAttribute(params string[] users) : base(typeof(PermissionRequirementFilter))
    {
        this.Arguments = new object[]
        {
            users.ToArray()
        };
    }
}