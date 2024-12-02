using Entity.Enum;
using Microsoft.AspNetCore.Mvc;
using WebCore.Filters;

namespace WebCore.Attributes;

public class PermissionAuthorizeAttribute : TypeFilterAttribute
{
    public PermissionAuthorizeAttribute(params UserPermissions[] permissions) : base(typeof(PermissionRequirementFilter))
    {
        Arguments =
        [
            permissions.Cast<int>().ToArray()
        ];
    }
}