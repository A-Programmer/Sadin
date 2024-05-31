namespace Sadin.Application.Roles.CreateRole;

public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 50);
        
        RuleFor(r => r.Description)
            .NotEmpty()
            .NotNull();
    }
}