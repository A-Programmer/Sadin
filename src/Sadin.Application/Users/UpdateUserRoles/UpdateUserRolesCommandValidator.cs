namespace Sadin.Application.Users.UpdateUserRoles;

public sealed class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    public UpdateUserRolesCommandValidator()
    {
        RuleFor(ur => ur.Roles)
            .Must(r => r.Length > 0);
    }
}