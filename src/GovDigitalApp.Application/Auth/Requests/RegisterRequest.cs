using System.ComponentModel.DataAnnotations;

namespace GovDigitalApp.Application.Auth.Requests;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    [StringLength(256, MinimumLength = 5)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(128, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@"^[\p{L}\p{M}'\- ]+$", ErrorMessage = "First name contains invalid characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@"^[\p{L}\p{M}'\- ]+$", ErrorMessage = "Last name contains invalid characters.")]
    public string LastName { get; set; } = string.Empty;
}
