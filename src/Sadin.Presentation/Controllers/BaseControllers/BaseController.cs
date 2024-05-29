using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sadin.Common.RequestOptions;
using Sadin.Common.Utilities;
using Sadin.Presentation.Constants;

namespace Sadin.Presentation.Controllers.BaseControllers;

[ApiController]
[Route(Routes.Root)]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;
    
    protected BaseController(ISender sender)
    {
        Sender = sender;
    }
    
    [NonAction]
    protected ActionResult PagedResult<T>(List<T> data, int pageIndex, int pageSize)
    {
        return Ok(new PaginatedList<T>(data, data.Count, pageIndex, pageSize));
    }
    
    protected string GetUserIp()
    {
        return Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }

}