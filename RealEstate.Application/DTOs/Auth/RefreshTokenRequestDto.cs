using System.ComponentModel.DataAnnotations;
namespace RealEstate.Application.DTOs.Auth;
public class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}