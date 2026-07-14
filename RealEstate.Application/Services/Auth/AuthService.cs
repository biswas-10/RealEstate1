using RealEstate.Application.DTOs.Auth;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Application.Settings;
using RealEstate.Domain.Common;
using RealEstate.Domain.Entities;
using Microsoft.Extensions.Options;

namespace RealEstate.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<JwtSettings> jwtOptions)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;

        _jwtSettings = jwtOptions.Value;
    }

    public async Task<Result<AuthResponseDto?>> LoginAsync(
        LoginRequestDto request)
    {
        // Find user by email.
        var user = await _userRepository
            .GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<AuthResponseDto?>
                .Failure(
                    Error.Validation(
                        "Login.Invalid",
                        "Invalid email or password."));
        }

        // Temporary password comparison.
        // Later replace with BCrypt password verification.
        if (user.PasswordHash != request.Password)
        {
            return Result<AuthResponseDto?>
                .Failure(
                    Error.Validation(
                        "Login.Invalid",
                        "Invalid email or password."));
        }

        // Generate JWT access token.
        var accessToken =
            _jwtTokenGenerator.GenerateToken(user);

        // Create refresh token.
        var refreshToken = new RefreshToken
        {
            Token = _refreshTokenGenerator.Generate(),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        // Save refresh token.
        await _refreshTokenRepository
            .AddAsync(refreshToken);

        await _refreshTokenRepository
            .SaveChangesAsync();

        return Result<AuthResponseDto?>
            .Success(
                CreateAuthResponse(
                    user,
                    accessToken,
                    refreshToken.Token)
            );
    }

    public async Task<Result<AuthResponseDto?>> RefreshAsync(
        RefreshTokenRequestDto request)
    {
        // Find refresh token from database.
        var storedToken =
            await _refreshTokenRepository
                .GetByTokenAsync(
                    request.RefreshToken);

        // Validate token.
        if (storedToken is null ||
            storedToken.IsRevoked ||
            storedToken.IsExpired)
        {
            return Result<AuthResponseDto?>
                .Failure(
                    Error.Validation(
                        "Refresh.Invalid",
                        "Invalid refresh token."));
        }

        // Load related user.
        var user =
            await _userRepository.GetByIdAsync(
                storedToken.UserId);

        if (user is null)
        {
            return Result<AuthResponseDto?>
                .Failure(
                    Error.Validation(
                        "Refresh.Invalid",
                        "User not found."));
        }

        // Revoke old refresh token.
        storedToken.IsRevoked = true;

        await _refreshTokenRepository
            .UpdateAsync(storedToken);

        // Create replacement refresh token.
        var newRefreshToken =
            new RefreshToken
            {
                Token =
                    _refreshTokenGenerator.Generate(),

                UserId = user.Id,

                ExpiresAt =
                    DateTime.UtcNow.AddDays(7),

                IsRevoked = false
            };

        await _refreshTokenRepository
            .AddAsync(newRefreshToken);

        await _refreshTokenRepository
            .SaveChangesAsync();

        // Generate new access token.
        var newAccessToken =
            _jwtTokenGenerator.GenerateToken(user);

        return Result<AuthResponseDto?>
            .Success(
                CreateAuthResponse(
                    user,
                    newAccessToken,
                    newRefreshToken.Token)
                );
    }

    private AuthResponseDto CreateAuthResponse(
        User user,
        string accessToken,
        string refreshToken)
    {
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,

            ExpiresAt = DateTime.UtcNow.AddMinutes(
                _jwtSettings.ExpiryMinutes),
            
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }
}