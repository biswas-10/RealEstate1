using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.Property;

public class PropertyResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public PropertyStatus Status { get; set; }
}