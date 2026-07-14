using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces.IRepo;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken refreshToken);
    Task SaveChangesAsync();
}