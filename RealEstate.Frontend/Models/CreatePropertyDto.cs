
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Frontend.Models;

public class CreatePropertyDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;
    
    [Range(typeof(decimal), "0.01", "54735432453442")]
    public decimal Price { get; set; }
}