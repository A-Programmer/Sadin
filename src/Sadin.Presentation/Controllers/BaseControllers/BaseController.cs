using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sadin.Presentation.Constants;

namespace Sadin.Presentation.Controllers.BaseControllers;

[ApiController]
[Route(Routes.Root)]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;
    
    public BaseController(ISender sender)
    {
        Sender = sender;
    }
    
    

}