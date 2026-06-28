
using RealEstate.Application.DTOs.User;

namespace RealEstate.Application.Interfaces.IServices;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllAsync();

    Task<UserDto> CreateAsync(
        CreateUserDto dto);
}