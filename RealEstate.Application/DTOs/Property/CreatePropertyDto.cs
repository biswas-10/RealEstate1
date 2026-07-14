using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Property;

public class CreatePropertyDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Address { get; set; } = string.Empty;
    
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Price { get; set; }
}