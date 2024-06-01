using Microsoft.AspNetCore.Authorization;
using Sadin.Presentation.Controllers.BaseControllers;

namespace Sadin.Presentation.Controllers;

[Authorize]
[ApiExplorerSettings(GroupName = SwaggerGroupLabels.General)]
public class TestController : BaseController
{
    public TestController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Successfully authenticated");
    }
}