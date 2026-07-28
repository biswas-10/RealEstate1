
using RealEstate.Domain.Enums;

namespace RealEstate.Frontend.Models;

public class PropertyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public PropertyStatus Status { get; set; }
}