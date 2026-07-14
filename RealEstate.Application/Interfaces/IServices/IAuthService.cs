using RealEstate.Application.DTOs.Auth;
using RealEstate.Domain.Common;

namespace RealEstate.Application.Interfaces.IServices;

public interface IAuthService
{
    Task<Result<AuthResponseDto?>> LoginAsync(
        LoginRequestDto request);

    Task<Result<AuthResponseDto?>> RefreshAsync(
        RefreshTokenRequestDto request);
}