using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Entity.Enums;
using Entity.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using WebCore.Constants;

namespace WebCore.Filters;

public class PermissionRequirementFilter(int[] requiredPermissionsCodes) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var rawStructuresId = context.HttpContext.User.FindFirstValue(CustomClaimNames.Structures)
                              ?? throw new UnauthorizedException("Unauthorized");

        if (string.IsNullOrEmpty(rawStructuresId))
            throw new UnauthorizedException("Unauthorized");

        var permissions = rawStructuresId!.Split(',').SelectMany(x =>
            {
                if (int.TryParse(x, out var structureId) &&
                    StaticCache.Permissions.TryGetValue(structureId, out var permissions) &&
                    permissions is {Count: > 0})
                    return permissions;
                return [];
            }).ToList();

        if (requiredPermissionsCodes.Any(x => permissions.All(pc => pc != x)))
            throw new AlreadyExistsException("Forbidden");

        return Task.CompletedTask;
    }
}