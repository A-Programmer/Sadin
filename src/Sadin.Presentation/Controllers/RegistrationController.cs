using Sadin.Application.Users.CheckUserExistence;
using Sadin.Application.Users.Register;
using Sadin.Common.CustomExceptions;
using Sadin.Presentation.Controllers.BaseControllers;

namespace Sadin.Presentation.Controllers;

public class RegistrationController : BaseController
{
    public RegistrationController(ISender sender) : base(sender)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        CheckUserExistenceQuery checkUserExistenceQuery = new(request.UserName);
        Guid? userId = await Sender.Send(checkUserExistenceQuery, cancellationToken);
        if (userId is not null)
            throw new KsDuplicatedUserException("A user with this information exist.");
        
        RegisterCommand registerUserCommand = new(request.UserName,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.ConfirmPassword);
        Guid? registerUserResult = await Sender.Send(registerUserCommand, cancellationToken);
        
        return Ok(registerUserResult);
    }
}