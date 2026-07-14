using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.Interfaces.IServices;

namespace RealEstate.Api.Controllers;

public class AgentController : ApiControllerBase
{
    private readonly IAgentService _agentService;

    public AgentController(
        IAgentService agentService)
    {
        ArgumentNullException.ThrowIfNull(agentService);
        _agentService = agentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var agents = await _agentService.GetAllAsync();
        return Success(
            agents,
            "Agents retrieved successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAgentDto dto)
    {
        var createdAgent =
            await _agentService.CreateAsync(dto);
        return CreatedSuccess(
            createdAgent,
            "Agent created successfully.");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAgentDto dto)
    {
        var updatedAgent =
            await _agentService.UpdateAsync(id, dto);
        if (updatedAgent is null)
        {
            return NotFoundResponse(
                $"Agent with Id {id} was not found.");
        }

        return Success(
            updatedAgent,
            "Agent updated successfully.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _agentService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFoundResponse(
                $"Agent with Id {id} was not found.");
        }

        return Success(
            true,
            "Agent deleted successfully");
    }
}