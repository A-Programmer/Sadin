using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Users;

namespace Sadin.Domain.Aggregates.Roles;

public sealed class Role : BaseEntity, IAggregateRoot
{
    public Role(string name,
        string? description)
    {
        if (!name.HasValue())
            throw new ArgumentNullException(nameof(name));
        Name = name;

        if (description.HasValue())
            Description = description;
    }
    
    public string Name { get; set; }
    public string? Description { get; set; } = string.Empty;

    private List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public void Update(string name,
        string? description)
    {
        if (!name.HasValue())
            throw new ArgumentNullException(nameof(name));
        Name = name;

        if (!description.HasValue())
            Description = description;
    }

    public void AddUserToRole(User user)
    {
        if (!_users.Contains(user))
            _users.Add(user);
    }

    public void RemoveUserFromRole(User user)
    {
        if (_users.Contains(user))
            _users.Remove(user);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Users);

        builder.HasData(new List<Role>()
        {
            new("Admin", "Admin Group"),
            new("User", "Users Group")
        });
    }
}