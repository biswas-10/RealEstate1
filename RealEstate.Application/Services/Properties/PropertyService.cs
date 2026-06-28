using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services.Properties;

public class PropertyService : IPropertyService
{
    public readonly IPropertyRepository _propertyRepository;
    // private IPropertyService _propertyServiceImplementation;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PropertyResponseDto?> GetByIdAsync(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property is null)
        {
            return null;
        }

        return MapToResponseDto(property);
    }

    public async Task<IEnumerable<PropertyResponseDto>> GetAllAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();
        return properties.Select(MapToResponseDto);
    }

    public async Task<PropertyResponseDto> CreateAsync(CreatePropertyDto dto)
    {
        var property = new Property
        {
            Title = dto.Title,
            Address = dto.Address,
            Price = dto.Price
        };

        await _propertyRepository.AddAsync(property);
        return MapToResponseDto(property);
    }

    public async Task<PropertyResponseDto?> UpdateAsync(
        int id,
        UpdatePropertyDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property is null)
        {
            return null;
        }

        property.Title = dto.Title;
        property.Address = dto.Address;
        property.Price = dto.Price;

        await _propertyRepository.UpdateAsync(property);
        return MapToResponseDto(property);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property is null)
        {
            return false;
        }

        await _propertyRepository.DeleteAsync(property);
        return true;
    }

    private static PropertyResponseDto MapToResponseDto(Property property)
    {
        return new PropertyResponseDto
        {
            Id = property.Id,
            Title = property.Title,
            Address = property.Address,
            Price = property.Price,
            Status = property.Status
        };
    }
}