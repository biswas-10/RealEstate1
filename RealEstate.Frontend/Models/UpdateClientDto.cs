using System.ComponentModel.DataAnnotations;

namespace RealEstate.Frontend.Models;

public class UpdateClientDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;
}