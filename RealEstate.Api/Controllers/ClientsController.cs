using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Client;
using RealEstate.Application.Interfaces.IServices;

namespace RealEstate.Api.Controllers;

public class ClientController : ApiControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        ArgumentNullException.ThrowIfNull(clientService);
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Success(
            clients,
            "Clients retrieved successfully");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null)
        {
            return NotFoundResponse(
                $"Client with Id {id} not found");
        }

        return Success(
            client,
            "Client retrieved successfully");
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateClientDto dto)
    {
        var createdClient =
            await _clientService.CreateAsync(dto);

        return CreatedSuccess(
            createdClient,
            "Client created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateClientDto dto)
    {
        var updatedClient =
            await _clientService.UpdateAsync(id, dto);

        if (updatedClient is null)
        {
            return NotFoundResponse(
                $"Client with Id {id} not found");
        }

        return Success(
            updatedClient,
            "Client updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _clientService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFoundResponse(
                $"Client with Id {id} not found");
        }

        return Success(
            true,
            "Client deleted successfully");
    }
}