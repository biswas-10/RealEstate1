

using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.IRepo;

public interface IAgentRepository
{
    Task<IEnumerable<Agent>> GetAllAsync();

    Task<Agent?> GetByIdAsync(int id);

    Task<Agent> CreateAsync(Agent agent);

    Task<Agent?> UpdateAsync(Agent agent);

    Task<bool> DeleteAsync(int id);
}

