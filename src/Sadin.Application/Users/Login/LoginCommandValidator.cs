namespace Sadin.Application.Users.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.UserName)
            .NotEmpty()
            .NotNull();
        
        RuleFor(l => l.Password)
            .NotEmpty()
            .NotNull();
    }
}