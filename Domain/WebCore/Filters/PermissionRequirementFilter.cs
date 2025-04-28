using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Entity.Exceptions;
using Entity.Exeptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace WebCore.Filters;

public class PermissionRequirementFilter(int[] requiredPermissionsCodes) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var rawStructureId = context.HttpContext.User.FindFirstValue(CustomClaimNames.Structure);

        if (!rawStructureId.IsNullOrEmpty() && int.TryParse(rawStructureId, out int structureId) && structureId == 0)
        {
            return Task.CompletedTask;
        }

        var rawPermissionCodes = context.HttpContext.User.FindFirstValue(CustomClaimNames.Structure);
        if (rawPermissionCodes.IsNullOrEmpty())
            throw new UnauthorizedException("Unauthorized");

        var permissionCodes = rawPermissionCodes?.Split(", ").Select(int.Parse);

        var anyNotMatched = requiredPermissionsCodes.Any(x => permissionCodes != null && permissionCodes.All(pc => pc != x));

        if (anyNotMatched)
            throw new AlreadyExistsException("Forbidden");

        return Task.CompletedTask;
    }
}