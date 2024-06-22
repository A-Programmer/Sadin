using System.Text.RegularExpressions;

namespace Sadin.Application.Users.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty()
            .NotNull()
            .Length(5, 50);

        RuleFor(u => u.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();

        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

        RuleFor(u => u.Roles)
            .NotEmpty()
            .NotNull()
            .Must(r => r.Length > 0);
    }
}