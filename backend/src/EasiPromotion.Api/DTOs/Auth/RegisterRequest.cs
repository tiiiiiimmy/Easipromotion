using System.ComponentModel.DataAnnotations;

namespace EasiPromotion.Api.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public required string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; set; }
} 