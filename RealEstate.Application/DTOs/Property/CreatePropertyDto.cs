namespace RealEstate.Application.DTOs.Property;

public class CreatePropertyDto
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
}