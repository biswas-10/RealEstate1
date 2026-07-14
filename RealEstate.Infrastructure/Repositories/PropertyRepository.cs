using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;

    public PropertyRepository(AppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        return await _context.Properties
            .AsNoTracking()
            .FirstOrDefaultAsync(property => property.Id == id);
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Properties
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Property property)
    {
        ArgumentNullException.ThrowIfNull(property);
        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Property property)
    {
        ArgumentNullException.ThrowIfNull(property);
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Property property)
    {
        ArgumentNullException.ThrowIfNull(property);
        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
    }
}