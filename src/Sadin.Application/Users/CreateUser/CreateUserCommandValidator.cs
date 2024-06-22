namespace Sadin.Application.Users.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
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

        RuleFor(u => u.Roles)
            .NotEmpty()
            .NotNull()
            .Must(x => x.Length > 0);
    }
}