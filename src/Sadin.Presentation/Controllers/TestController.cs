using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sadin.Presentation.Controllers;

[Authorize]
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