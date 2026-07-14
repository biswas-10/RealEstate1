using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(client => client.Id == id);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        await _context.Clients.AddAsync(client);
        await _context.Clients.AddAsync(client);
        return client;
    }

    public async Task<Client?> UpdateAsync(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == id);
        if (client is null)
        {
            return false;
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}