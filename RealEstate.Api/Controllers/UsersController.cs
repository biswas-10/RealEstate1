using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.User;
using RealEstate.Application.Interfaces.IServices;

namespace RealEstate.Api.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(
        IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    // Returns all users.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users =
            await _userService.GetAllAsync();

        return Success(
            users,
            "Users retrieved successfully.");
    }

    // GET: api/users/{id}
    // Returns a single user.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var user =
            await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return NotFoundResponse(
                $"User with Id {id} was not found.");
        }

        return Success(
            user,
            "User retrieved successfully.");
    }

    // POST: api/users
    // Creates a new user.
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserDto dto)
    {
        var user =
            await _userService.CreateAsync(dto);

        return CreatedSuccess(
            user,
            "User created successfully.");
    }
}