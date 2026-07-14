using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.IRepo;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client?> GetByIdAsync(int id);

    Task<Client> CreateAsync(Client client);

    Task<Client?> UpdateAsync(Client client);

    Task<bool> DeleteAsync(int id);
}