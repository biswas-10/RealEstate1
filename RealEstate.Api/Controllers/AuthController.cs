
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Auth;
using RealEstate.Application.Interfaces.IServices;

namespace RealEstate.Api.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }
    
    // POST: api/Auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequestDto request)
    {
        var result =
            await _authService.LoginAsync(request);

        if (!result.IsSuccess)
        {
            return UnauthorizedResponse(
                result.Error?.Message
                ?? "Invalid email or password.");
        }

        return Success(
            result.Value!,
            "Login successful.");
    }
    
    //POST: api/Auth/refresh
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        RefreshTokenRequestDto request)
    {
        var result =
            await _authService.RefreshAsync(request);

        if (!result.IsSuccess)
        {
            return UnauthorizedResponse(
                result.Error?.Message
                ?? "Invalid refresh token.");
        }

        return Success(
            result.Value!,
            "Token refreshed successfully.");
    }
}

