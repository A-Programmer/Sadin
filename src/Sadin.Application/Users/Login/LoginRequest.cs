using System.ComponentModel.DataAnnotations;

namespace Sadin.Application.Users.Login;

public record LoginRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}