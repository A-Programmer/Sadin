namespace Sadin.Application.Users.DeleteUser;

public record DeleteUserCommand(Guid Id) : ICommand;