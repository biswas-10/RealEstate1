using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        return await _context.RefreshTokens
            .AsNoTracking()
            .Include(refreshToken => refreshToken.User)
            .FirstOrDefaultAsync(refreshToken => refreshToken.Token == token);
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);
        _context.RefreshTokens.Update(refreshToken);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}