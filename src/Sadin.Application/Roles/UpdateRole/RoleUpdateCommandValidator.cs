namespace Sadin.Application.Roles.UpdateRole;

public sealed class RoleUpdateCommandValidator : AbstractValidator<RoleUpdateResponse>
{
    public RoleUpdateCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 5);

        RuleFor(r => r.Description)
            .NotNull();
    }
}