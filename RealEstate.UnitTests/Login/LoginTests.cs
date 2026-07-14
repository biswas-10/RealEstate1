using Microsoft.Extensions.Options;
using Moq;
using RealEstate.Application.DTOs.Auth;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Application.Services.Auth;
using RealEstate.Application.Settings;
using RealEstate.Domain.Entities;

namespace RealEstate.UnitTests.Login;

public class LoginTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator = new();
    private readonly Mock<IRefreshTokenGenerator> _refreshTokenGenerator = new();
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepository = new();

    private readonly AuthService _authService;

    public LoginTests()
    {
        var jwtSettings = Options.Create(
            new JwtSettings
            {
                Key = "12345678901234567890123456789012",
                Issuer = "RealEstate",
                Audience = "RealEstate",
                ExpiryMinutes = 60
            });

        _authService = new AuthService(
            _userRepository.Object,
            _jwtTokenGenerator.Object,
            _refreshTokenGenerator.Object,
            _refreshTokenRepository.Object,
            jwtSettings);
    }

    [Fact]
    public async Task LoginAsync_ShouldFail_WhenUserDoesNotExist()
    {
        _userRepository
            .Setup(x => x.GetByEmailAsync("test@test.com"))
            .ReturnsAsync((User?)null);

        var result = await _authService.LoginAsync(
            new LoginRequestDto
            {
                Email = "test@test.com",
                Password = "123456"
            });

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        _jwtTokenGenerator.Verify(
            x => x.GenerateToken(It.IsAny<User>()),
            Times.Never);

        _refreshTokenRepository.Verify(
            x => x.AddAsync(It.IsAny<RefreshToken>()),
            Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ShouldFail_WhenPasswordIsInvalid()
    {
        var user = new User
        {
            Id = 1,
            FullName = "Test User",
            Email = "test@test.com",
            PasswordHash = "correct-password",
            Role = "User"
        };

        _userRepository
            .Setup(x => x.GetByEmailAsync(user.Email))
            .ReturnsAsync(user);

        var result = await _authService.LoginAsync(
            new LoginRequestDto
            {
                Email = user.Email,
                Password = "wrong-password"
            });

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        _jwtTokenGenerator.Verify(
            x => x.GenerateToken(It.IsAny<User>()),
            Times.Never);

        _refreshTokenRepository.Verify(
            x => x.AddAsync(It.IsAny<RefreshToken>()),
            Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnTokens_WhenCredentialsAreValid()
    {
        var user = new User
        {
            Id = 1,
            FullName = "Test User",
            Email = "test@test.com",
            PasswordHash = "123456",
            Role = "Admin"
        };

        _userRepository
            .Setup(x => x.GetByEmailAsync(user.Email))
            .ReturnsAsync(user);

        _jwtTokenGenerator
            .Setup(x => x.GenerateToken(user))
            .Returns("access-token");

        _refreshTokenGenerator
            .Setup(x => x.Generate())
            .Returns("refresh-token");

        var result = await _authService.LoginAsync(
            new LoginRequestDto
            {
                Email = user.Email,
                Password = "123456"
            });

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(
            "access-token",
            result.Value!.AccessToken);

        Assert.Equal(
            "refresh-token",
            result.Value.RefreshToken);

        Assert.Equal(
            user.Id,
            result.Value.UserId);

        Assert.Equal(
            user.Email,
            result.Value.Email);

        _refreshTokenRepository.Verify(
            x => x.AddAsync(It.IsAny<RefreshToken>()),
            Times.Once);

        _refreshTokenRepository.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
    
        [Fact]
    public async Task RefreshAsync_ShouldFail_WhenTokenDoesNotExist()
    {
        _refreshTokenRepository
            .Setup(x => x.GetByTokenAsync("invalid-token"))
            .ReturnsAsync((RefreshToken?)null);

        var result = await _authService.RefreshAsync(
            new RefreshTokenRequestDto
            {
                RefreshToken = "invalid-token"
            });

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        _jwtTokenGenerator.Verify(
            x => x.GenerateToken(It.IsAny<User>()),
            Times.Never);

        _refreshTokenRepository.Verify(
            x => x.UpdateAsync(It.IsAny<RefreshToken>()),
            Times.Never);
    }

    [Fact]
    public async Task RefreshAsync_ShouldFail_WhenTokenIsRevoked()
    {
        var refreshToken = new RefreshToken
        {
            Token = "revoked-token",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            IsRevoked = true
        };

        _refreshTokenRepository
            .Setup(x => x.GetByTokenAsync(refreshToken.Token))
            .ReturnsAsync(refreshToken);

        var result = await _authService.RefreshAsync(
            new RefreshTokenRequestDto
            {
                RefreshToken = refreshToken.Token
            });

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        _jwtTokenGenerator.Verify(
            x => x.GenerateToken(It.IsAny<User>()),
            Times.Never);
    }

    [Fact]
    public async Task RefreshAsync_ShouldFail_WhenTokenIsExpired()
    {
        var refreshToken = new RefreshToken
        {
            Token = "expired-token",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(-1),
            IsRevoked = false
        };

        _refreshTokenRepository
            .Setup(x => x.GetByTokenAsync(refreshToken.Token))
            .ReturnsAsync(refreshToken);

        var result = await _authService.RefreshAsync(
            new RefreshTokenRequestDto
            {
                RefreshToken = refreshToken.Token
            });

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        _jwtTokenGenerator.Verify(
            x => x.GenerateToken(It.IsAny<User>()),
            Times.Never);
    }

    [Fact]
    public async Task RefreshAsync_ShouldFail_WhenUserDoesNotExist()
    {
        var refreshToken = new RefreshToken
        {
            Token = "valid-token",
            UserId = 100,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            IsRevoked = false
        };

        _refreshTokenRepository
            .Setup(x => x.GetByTokenAsync(refreshToken.Token))
            .ReturnsAsync(refreshToken);

        _userRepository
            .Setup(x => x.GetByIdAsync(refreshToken.UserId))
            .ReturnsAsync((User?)null);

        var result = await _authService.RefreshAsync(
            new RefreshTokenRequestDto
            {
                RefreshToken = refreshToken.Token
            });

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
    }
    
        [Fact]
    public async Task RefreshAsync_ShouldGenerateNewTokens_WhenRefreshTokenIsValid()
    {
        var storedToken = new RefreshToken
        {
            Token = "old-refresh-token",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            IsRevoked = false
        };

        var user = new User
        {
            Id = 1,
            FullName = "Test User",
            Email = "test@test.com",
            PasswordHash = "123456",
            Role = "Admin"
        };

        _refreshTokenRepository
            .Setup(x => x.GetByTokenAsync(storedToken.Token))
            .ReturnsAsync(storedToken);

        _userRepository
            .Setup(x => x.GetByIdAsync(user.Id))
            .ReturnsAsync(user);

        _jwtTokenGenerator
            .Setup(x => x.GenerateToken(user))
            .Returns("new-access-token");

        _refreshTokenGenerator
            .Setup(x => x.Generate())
            .Returns("new-refresh-token");

        var result = await _authService.RefreshAsync(
            new RefreshTokenRequestDto
            {
                RefreshToken = storedToken.Token
            });

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(
            "new-access-token",
            result.Value!.AccessToken);

        Assert.Equal(
            "new-refresh-token",
            result.Value.RefreshToken);

        Assert.True(storedToken.IsRevoked);

        _refreshTokenRepository.Verify(
            x => x.UpdateAsync(storedToken),
            Times.Once);

        _refreshTokenRepository.Verify(
            x => x.AddAsync(It.IsAny<RefreshToken>()),
            Times.Once);

        _refreshTokenRepository.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);

        _jwtTokenGenerator.Verify(
            x => x.GenerateToken(user),
            Times.Once);

        _refreshTokenGenerator.Verify(
            x => x.Generate(),
            Times.Once);
    }
}