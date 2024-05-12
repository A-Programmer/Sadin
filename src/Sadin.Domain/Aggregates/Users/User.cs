using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common.Utilities;
using Sadin.Common.CustomExceptions;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Domain.Aggregates.Users;

public sealed class User : BaseEntity, IAggregateRoot
{
    public User(string userName,
        string hashedPassword,
        string email,
        string phoneNumber)
    {
        if (!userName.HasValue())
            throw new ArgumentNullException(nameof(userName));
        UserName = userName;
        
        if (!hashedPassword.HasValue())
            throw new ArgumentNullException(nameof(hashedPassword));
        HashedPassword = hashedPassword;
        
        if (!email.HasValue())
            throw new ArgumentNullException(nameof(email));
        if (!email.IsValidEmail())
            throw new KsInvalidEmailAddressException();
        Email = email;
        
        if (!phoneNumber.HasValue())
            throw new ArgumentNullException(nameof(phoneNumber));
        if (!phoneNumber.IsValidMobile())
            throw new KsInvalidPhoneNumberException();
        PhoneNumber = phoneNumber;
    }
    
    public string UserName { get; set; }
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    private List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles;

    public void Update(string email,
        string phoneNumber)
    {
        if (email.HasValue())
            Email = email;
        
        if (!phoneNumber.HasValue())
            PhoneNumber = phoneNumber;
    }

    public void UpdatePassword(string hashedPassword)
    {
        if (!hashedPassword.HasValue())
            throw new ArgumentNullException(nameof(hashedPassword));
        HashedPassword = hashedPassword;
    }

    public void AssignRoles(IEnumerable<Role> roles)
    {
        foreach (Role role in roles)
        {
            if (!_roles.Contains(role))
                _roles.Add(role);
        }
    }

    public void UnAssignRoles(IEnumerable<Role> roles) =>
        _roles.RemoveAll(role => _roles.Contains(role));
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Roles);
    }
}