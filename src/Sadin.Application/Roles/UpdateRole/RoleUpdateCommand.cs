namespace Sadin.Application.Roles.UpdateRole;

public sealed class RoleUpdateCommand : ICommand<RoleUpdateResponse>
{
    public RoleUpdateCommand(Guid id, string name, string description)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
}