namespace Sadin.Application.Roles.GetRoleById;

public sealed class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .NotNull();
    }
}