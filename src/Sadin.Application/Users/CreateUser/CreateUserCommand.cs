namespace Sadin.Application.Users.CreateUser;

public record CreateUserCommand(string UserName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword,
    string[] Roles) : ICommand<CreateUserResponse>;