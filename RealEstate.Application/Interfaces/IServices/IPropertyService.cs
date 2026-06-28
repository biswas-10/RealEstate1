using RealEstate.Application.DTOs.Property;

namespace RealEstate.Application.Interfaces.IServices;

public interface IPropertyService
{
    Task<PropertyResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<PropertyResponseDto>> GetAllAsync();
    Task<PropertyResponseDto> CreateAsync(CreatePropertyDto dto);
    Task<PropertyResponseDto?> UpdateAsync(int id, UpdatePropertyDto dto);
    Task<bool> DeleteAsync(int id);
}