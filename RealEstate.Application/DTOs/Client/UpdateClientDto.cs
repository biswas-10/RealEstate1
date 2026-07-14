using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Client;

public class UpdateClientDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    public string Phone { get; set; } = string.Empty;
}