namespace Sadin.Application.Users.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty()
            .NotNull()
            .Length(5, 16);

        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .NotNull();

        RuleFor(u => u.Password)
            .NotEmpty()
            .NotNull()
            .Length(6,32)
            .Equal(u => u.ConfirmPassword);
    }
}