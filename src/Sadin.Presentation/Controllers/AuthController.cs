using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sadin.Application.Users.CheckUserExistence;
using Sadin.Application.Users.Login;
using Sadin.Common.CustomExceptions;

namespace Sadin.Presentation.Controllers;

public class AuthController : BaseController
{
    public AuthController(ISender sender) : base(sender)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        CheckUserExistenceQuery checkUserExistenceQuery = new(request.UserName);
        
        Guid? userId = await Sender.Send(checkUserExistenceQuery, cancellationToken);
        if (userId is null)
            throw new KsAuthenticationException("Invalid Username or Password");

        LoginCommand command = new(request.UserName, request.Password);

        var token = await Sender.Send(command, cancellationToken);

        return Ok(token.Token);
    }
}