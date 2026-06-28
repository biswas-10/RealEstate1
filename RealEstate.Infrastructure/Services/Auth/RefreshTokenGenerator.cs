using System.Security.Cryptography;
using RealEstate.Application.Interfaces.IServices;

namespace RealEstate.Infrastructure.Services.Auth;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
}