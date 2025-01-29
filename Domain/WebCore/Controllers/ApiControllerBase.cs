using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected long UserId => long.TryParse(User.FindFirstValue(CustomClaimNames.UserId), out var userId) ? userId : 0;
}