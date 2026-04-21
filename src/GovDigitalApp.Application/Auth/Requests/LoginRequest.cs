using System.ComponentModel.DataAnnotations;

namespace GovDigitalApp.Application.Auth.Requests;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    [StringLength(256, MinimumLength = 5)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(128, MinimumLength = 1)]
    public string Password { get; set; } = string.Empty;
}
