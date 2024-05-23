using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Sadin.Presentation.Controllers.BaseControllers;

[Authorize]
public abstract class SecureBaseController : BaseController
{
    protected SecureBaseController(ISender sender)
        : base(sender)
    {
    }
    
    protected string UserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
    protected string UserEmail => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
    protected List<string> UserRoles => User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
    protected Dictionary<string, string> UserClaims => User.Claims.ToDictionary(x => x.Type, x => x.Value);
    protected bool IsAdmin => UserRoles.Contains("SysAdmin");

    protected bool IsInRole(string role)
    {
        return UserRoles.Contains(role);
    }

    protected bool HasPermission(string resourceId, bool adminPermitted = true)
    {
        return (IsAdmin && adminPermitted) || UserId == resourceId;
    }
    
    protected string GetUserIp()
    {
        return Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }
}