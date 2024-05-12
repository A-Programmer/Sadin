using System.ComponentModel.DataAnnotations;

namespace Sadin.Application.Users.Register;

public record RegisterRequest
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
}