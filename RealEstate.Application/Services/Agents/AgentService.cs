
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Domain.Entities;

namespace  RealEstate.Application.Services.Agents;

public class AgentService : IAgentService
{
    private readonly IAgentRepository _agentRepository;

    public AgentService(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task<AgentResponseDto?> GetByIdAsync(int id)
    {
        var agent = await _agentRepository.GetByIdAsync(id);

        if (agent is null)
        {
            return null;
        }

        return MapToResponseDto(agent);
    }

    public async Task<IEnumerable<AgentResponseDto>> GetAllAsync()
    {
        var agents = await _agentRepository.GetAllAsync();
        return agents.Select(MapToResponseDto);
    }

    public async Task<AgentResponseDto> CreateAsync(
        CreateAgentDto dto)
    {
        var agent = new Agent
        {
            FullName = dto.FullName,
            Email = dto.Email
        };
        var createdAgent =
            await _agentRepository.CreateAsync(agent);
        return MapToResponseDto(createdAgent);
    }

    public async Task<AgentResponseDto?> UpdateAsync(
        int id,
        UpdateAgentDto dto)
    {
        var agent = await _agentRepository.GetByIdAsync(id);

        if (agent is null)
        {
            return null;
        }

        agent.FullName = dto.FullName;
        agent.Email = dto.Email;

        var updatedAgent =
            await _agentRepository.UpdateAsync(agent);

        if (updatedAgent is null)
        {
            return null;
        }

        return MapToResponseDto(updatedAgent);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _agentRepository.DeleteAsync(id);
    }

    private static AgentResponseDto MapToResponseDto(
        Agent agent)
    {
        return new AgentResponseDto
        {
            Id = agent.Id,
            FullName = agent.FullName,
            Email = agent.Email
        };
    }
}












