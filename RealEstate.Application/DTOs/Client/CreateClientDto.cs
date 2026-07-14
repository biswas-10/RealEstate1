using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Client;

public class CreateClientDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Phone { get; set; } = string.Empty;
}