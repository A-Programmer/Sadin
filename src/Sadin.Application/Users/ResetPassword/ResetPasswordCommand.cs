namespace Sadin.Application.Users.ResetPassword;

public record ResetPasswordCommand(Guid Id,
    string NewPassword) : ICommand;