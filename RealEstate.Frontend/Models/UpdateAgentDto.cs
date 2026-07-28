using System.ComponentModel.DataAnnotations;

namespace RealEstate.Frontend.Models;

public class UpdateAgentDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}