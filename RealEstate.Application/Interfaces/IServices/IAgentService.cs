

using RealEstate.Application.DTOs.Agent;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.IServices;

public interface IAgentService
{
    Task<IEnumerable<AgentResponseDto>> GetAllAsync();

    Task<AgentResponseDto?> GetByIdAsync(int id);

    Task<AgentResponseDto> CreateAsync(
        CreateAgentDto dto);

    Task<AgentResponseDto?> UpdateAsync(
        int id,
        UpdateAgentDto dto);

    Task<bool> DeleteAsync(int id);
}

