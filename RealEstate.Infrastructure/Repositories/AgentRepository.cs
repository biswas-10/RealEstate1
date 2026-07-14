using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Repositories;

public class AgentRepository : IAgentRepository
{
    private readonly AppDbContext _context;

    public AgentRepository(AppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<IEnumerable<Agent>> GetAllAsync()
    {
        return await _context.Agents
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Agent?> GetByIdAsync(int id)
    {
        return await _context.Agents
            .AsNoTracking()
            .FirstOrDefaultAsync(agent => agent.Id == id);
    }

    public async Task<Agent> CreateAsync(Agent agent)
    {
        ArgumentNullException.ThrowIfNull(agent);
        await _context.Agents.AddAsync(agent);
        await _context.SaveChangesAsync();
        return agent;
    }

    public async Task<Agent?> UpdateAsync(Agent agent)
    {
        ArgumentNullException.ThrowIfNull(agent);
        _context.Agents.Update(agent);
        await _context.SaveChangesAsync();
        return agent;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var agent = await _context.Agents
            .FirstOrDefaultAsync(agent => agent.Id == id);
        if (agent is null)
        {
            return false;
        }

        _context.Agents.Remove(agent);
        await _context.SaveChangesAsync();
        return true;
    }
}
