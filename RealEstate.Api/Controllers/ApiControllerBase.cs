using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Contracts;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult Success<T>(
        T data,
        string message = "Request completed successfully")
    {
        return Ok(
            ApiResponse<T>.SuccessResponse(
                data, 
                message));
    }

    protected IActionResult CreatedSuccess<T>(
        T data,
        string message = "Resource created successfully")
    {
        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<T>.SuccessResponse(
                data,
                message));
    }

    protected IActionResult BadRequestResponse(
        string message)
    {
        return BadRequest(
            ApiResponse<object>.FailureResponse(
                message));
    }

    protected IActionResult NotFoundResponse(
        string message)
    {
        return NotFound(
            ApiResponse<object>.FailureResponse(
                message));
    }

    protected IActionResult UnauthorizedResponse(
        string message = "Unauthorized")
    {
        return Unauthorized(
            ApiResponse<object>.FailureResponse(
                message));
    }
}