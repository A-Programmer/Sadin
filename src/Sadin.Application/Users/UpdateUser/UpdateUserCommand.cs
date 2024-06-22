namespace Sadin.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid Id,
        string UserName,
        string Email,
        string PhoneNumber,
        string[] Roles)
    : ICommand<UpdateUserResponse>;