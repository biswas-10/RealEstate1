using RealEstate.Application.DTOs.User;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Interfaces.IServices;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Return a single user.
    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user =
            await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        return MapToDto(user);
    }

    // Return all users.
    // IUserRepository currently does not expose GetAllAsync().
    // We will add that later if needed.
    
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        // Load every user from the database.
        var users = await _userRepository.GetAllAsync();

        // Convert each entity into a DTO.
        return users.Select(MapToDto);
    }
    
    // Create a new user.
    public async Task<UserDto> CreateAsync(
        CreateUserDto dto)
    {
        // Prevent duplicate email registration.
        var emailExists =
            await _userRepository
                .EmailExistsAsync(dto.Email);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "Email already exists.");
        }

        var user = new User
        {
            FullName = dto.FullName,

            Email = dto.Email,

            // TEMPORARY:
            // Later replace with BCrypt hashing.
            PasswordHash = dto.Password,

            Role = dto.Role
        };

        await _userRepository.AddAsync(user);

        return MapToDto(user);
    }

    // Centralized mapping method.
    private static UserDto MapToDto(
        User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}