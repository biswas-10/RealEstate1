using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.IServices;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}

public interface IRefreshTokenGenerator
{
    string Generate();
}