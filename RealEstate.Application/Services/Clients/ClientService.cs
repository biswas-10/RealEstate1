using RealEstate.Application.DTOs.Client;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services.Clients;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(
        IClientRepository clientRepository)
    {
        ArgumentNullException.ThrowIfNull(clientRepository);
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllAsync()
    {
        var clients =
            await _clientRepository.GetAllAsync();
        return clients.Select(MapToResponseDto);
    }

    public async Task<ClientResponseDto?> GetByIdAsync(int id)
    {
        var client =
            await _clientRepository.GetByIdAsync(id);
        if (client is null)
        {
            return null;
        }

        return MapToResponseDto(client);
    }

    public async Task<ClientResponseDto> CreateAsync(CreateClientDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        var client = new Client
        {
            FullName = dto.FullName,
            Phone = dto.Phone
        };
        var createdClient =
            await _clientRepository.CreateAsync(client);
        return MapToResponseDto(createdClient);
    }

    public async Task<ClientResponseDto?> UpdateAsync(
        int id,
        UpdateClientDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        var client =
            await _clientRepository.GetByIdAsync(id);
        if (client is null)
        {
            return null;
        }
        client.FullName = dto.FullName;
        client.Phone = dto.Phone;

        var updatedClient =
            await _clientRepository.UpdateAsync(client);

        if (updatedClient is null)
        {
            return null;
        }

        return MapToResponseDto(updatedClient);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _clientRepository.DeleteAsync(id);
    }


    private static ClientResponseDto MapToResponseDto(
        Client client)
    {
        return new ClientResponseDto
        {
            Id = client.Id,
            FullName = client.FullName,
            Phone = client.Phone
        };
    }
}