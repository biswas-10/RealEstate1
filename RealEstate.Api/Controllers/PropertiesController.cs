using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Interfaces.IServices;

namespace RealEstate.Api.Controllers;

public class PropertiesController : ApiControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        ArgumentNullException.ThrowIfNull(propertyService);
        _propertyService = propertyService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _propertyService.GetAllAsync();
        return Success(
            properties,
            "Properties retrieved successfully");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);

        if (property is null)
        {
            return NotFoundResponse(
                $"Property with Id {id} was not found");
        }

        return Success(
            property,
            "Property retrieved successfully");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePropertyDto dto)
    {
        var createdProperty = await _propertyService.CreateAsync(dto);

        return CreatedSuccess(
            createdProperty,
            "Property created successfully");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePropertyDto dto)
    {
        var updatedProperty = 
            await _propertyService.UpdateAsync(id, dto);
        
        if (updatedProperty is null)
        {
            return NotFoundResponse(
                $"Property with Id {id} was not found.");
        }

        return Success(
            updatedProperty,
            "Property updated successfully");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = 
            await _propertyService.DeleteAsync(id);
        
        if (!deleted)
        {
            return NotFoundResponse(
                $"Property with Id {id} was not found.");
        }

        return Success(
            true,
            "Property deleted successfully");
    }
}