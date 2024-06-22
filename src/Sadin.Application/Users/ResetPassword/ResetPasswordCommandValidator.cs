namespace Sadin.Application.Users.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(rp => rp.Id)
            .NotEmpty()
            .NotNull();
        
        RuleFor(rp => rp.NewPassword)
            .NotEmpty()
            .NotNull()
            .Length(6, 30);
    }
}