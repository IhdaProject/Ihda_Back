using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Entity.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected long UserId => long.TryParse(User.FindFirstValue(CustomClaimNames.UserId), out var userId) ? userId : 0;
    protected long StructureId => long.TryParse(User.FindFirstValue(CustomClaimNames.Structures), out var structureId) ? structureId : 0;
    protected string Jti => User.FindFirstValue(JwtRegisteredClaimNames.Jti) ?? string.Empty;
}