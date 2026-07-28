
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Frontend.Models;

public class UpdatePropertyDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;
    
    [Range(typeof(decimal),"0.01","6543563444564324")]
    public decimal Price { get; set; }
}