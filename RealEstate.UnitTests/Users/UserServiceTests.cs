using Moq;
using RealEstate.Application.DTOs.User;
using RealEstate.Application.Interfaces.IRepo;
using RealEstate.Application.Services.Users;
using RealEstate.Domain.Entities;

namespace RealEstate.UnitTests.Users;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock =
            new Mock<IUserRepository>();

        _userService =
            new UserService(
                _userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((User?)null);

        // Act
        var result =
            await _userService.GetByIdAsync(1);

        // Assert
        Assert.Null(result);

        _userRepositoryMock.Verify(
            x => x.GetByIdAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            FullName = "John Doe",
            Email = "john@test.com",
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        };

        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        // Act
        var result =
            await _userService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(user.Id, result!.Id);
        Assert.Equal(user.FullName, result.FullName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Role, result.Role);

        _userRepositoryMock.Verify(
            x => x.GetByIdAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new()
            {
                Id = 1,
                FullName = "John",
                Email = "john@test.com",
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = 2,
                FullName = "Jane",
                Email = "jane@test.com",
                Role = "User",
                CreatedAt = DateTime.UtcNow
            }
        };

        _userRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(users);

        // Act
        var result =
            await _userService.GetAllAsync();

        // Assert
        var resultList = result.ToList();

        Assert.Equal(2, resultList.Count);

        Assert.Equal("John", resultList[0].FullName);
        Assert.Equal("Jane", resultList[1].FullName);

        _userRepositoryMock.Verify(
            x => x.GetAllAsync(),
            Times.Once);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FullName = "John Doe",
            Email = "john@test.com",
            Password = "123456",
            Role = "Admin"
        };

        _userRepositoryMock
            .Setup(x => x.EmailExistsAsync(dto.Email))
            .ReturnsAsync(true);

        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _userService.CreateAsync(dto));

        Assert.Equal(
            "Email already exists.",
            exception.Message);

        _userRepositoryMock.Verify(
            x => x.EmailExistsAsync(dto.Email),
            Times.Once);

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<User>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateUser_WhenEmailDoesNotExist()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FullName = "John Doe",
            Email = "john@test.com",
            Password = "123456",
            Role = "Admin"
        };

        _userRepositoryMock
            .Setup(x => x.EmailExistsAsync(dto.Email))
            .ReturnsAsync(false);

        _userRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        var result =
            await _userService.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(dto.FullName, result.FullName);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.Role, result.Role);

        _userRepositoryMock.Verify(
            x => x.EmailExistsAsync(dto.Email),
            Times.Once);

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<User>()),
            Times.Once);
    }
}
