namespace Sadin.Application.Roles.CreateRole;

public sealed class CreateRoleCommand : ICommand<CreateRoleResponse>
{
    public CreateRoleCommand(string name, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
}