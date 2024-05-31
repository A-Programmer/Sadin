namespace Sadin.Application.Users.Register;

public sealed record RegisterCommand(string UserName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword) : ICommand<Guid>;