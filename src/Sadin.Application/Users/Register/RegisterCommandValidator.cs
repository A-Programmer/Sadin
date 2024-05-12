namespace Sadin.Application.Users.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty()
            .NotNull()
            .Length(5, 16);

        RuleFor(r => r.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .NotNull();

        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull()
            .Length(6,32)
            .Equal(r => r.ConfirmPassword);
    }
}