using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.IRepo;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(int id);
    Task<IEnumerable<Property>> GetAllAsync();
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(Property property);
}