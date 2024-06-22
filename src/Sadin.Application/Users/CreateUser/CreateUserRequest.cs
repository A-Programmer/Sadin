using System.ComponentModel.DataAnnotations;

namespace Sadin.Application.Users.CreateUser;

public sealed class CreateUserRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    public string[] Roles { get; set; }
}