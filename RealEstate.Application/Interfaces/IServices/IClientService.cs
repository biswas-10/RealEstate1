

using RealEstate.Application.DTOs.Client;

namespace RealEstate.Application.Interfaces.IServices;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>> GetAllAsync();

    Task<ClientResponseDto?> GetByIdAsync(int id);

    Task<ClientResponseDto> CreateAsync(
        CreateClientDto dto);
    
    Task<ClientResponseDto?> UpdateAsync(
        int id,
        UpdateClientDto dto);

    Task<bool> DeletedAsync(int id);
}

