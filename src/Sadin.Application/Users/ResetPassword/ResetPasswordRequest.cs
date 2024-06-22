using System.ComponentModel.DataAnnotations;

namespace Sadin.Application.Users.ResetPassword;

public sealed class ResetPasswordRequest
{
    [Required]
    public string NewPassword { get; set; }
    
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; }
    
}