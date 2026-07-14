using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    // Find a user by Id.
    // Used during refresh token validation.
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    // Find a user by email.
    // Used during login.
    public async Task<User?> GetByEmailAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                user => user.Email == email);
    }

    // Create a new user.
    public async Task AddAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
    }

    // Update an existing user.
    public async Task UpdateAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        _context.Users.Update(user);

        await _context.SaveChangesAsync();
    }

    // Check whether an email already exists.
    // Useful before user registration.
    public async Task<bool> EmailExistsAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        
        return await _context.Users
            .AnyAsync(
                user => user.Email == email);
    }
}