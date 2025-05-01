using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Entity.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using WebCore.Constants;

namespace WebCore.Filters;

public class PermissionRequirementFilter(int[] requiredPermissionsCodes) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var rawStructureId = context.HttpContext.User.FindFirstValue(CustomClaimNames.Structure);

        if (rawStructureId.IsNullOrEmpty() ||
            !int.TryParse(rawStructureId, out var structureId) ||
            structureId == 0 ||
            !StaticCache.Permissions.TryGetValue(structureId, out var permissions) ||
            !(permissions.Count > 0))
            throw new UnauthorizedException("Unauthorized");

        var anyNotMatched = requiredPermissionsCodes.Any(x => permissions.All(pc => pc != x));

        if (anyNotMatched)
            throw new AlreadyExistsException("Forbidden");

        return Task.CompletedTask;
    }
}