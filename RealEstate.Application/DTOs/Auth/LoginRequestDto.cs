using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Auth;
public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}