namespace RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;

public class Property
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0m;
    public PropertyStatus Status { get; set; } = PropertyStatus.Available;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}