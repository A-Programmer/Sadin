namespace Sadin.Presentation.Controllers.BaseControllers;

[ApiController]
[Route(Routes.Root)]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;
    
    protected BaseController(ISender sender)
    {
        Sender = sender;
    }
    
    protected string GetUserIp()
    {
        return Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;
    }

}