using CashPilot.Application.Interfaces.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Interfaces.Services.Caching;
using CashPilot.Application.Services;
using CashPilot.Application.Tests.Common;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.Entities;
using CashPilot.Domain.Exceptions;
using Moq;

namespace CashPilot.Application.Tests.Services;

public class UserServiceTests : TestBase
{
    private readonly UserService _userService;
    
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IVerificationService> _verificationServiceMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IResetPasswordAttemptService> _resetPasswordAttemptServiceMock;
    private readonly Mock<IEmailHelper> _emailHelperMock;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _verificationServiceMock = new Mock<IVerificationService>();
        _tokenServiceMock = new Mock<ITokenService>();
        _resetPasswordAttemptServiceMock = new Mock<IResetPasswordAttemptService>();
        _emailHelperMock = new Mock<IEmailHelper>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _verificationServiceMock.Object,
            _tokenServiceMock.Object,
            _resetPasswordAttemptServiceMock.Object,
            _emailHelperMock.Object,
            Mapper);
    }

    [Fact]
    public async Task CreateUser_ValidUser_ShouldReturnUserDto()
    {
        var createUserStub = new CreateUserDto
        {
            Name = "Test Test",
            Email = "test@email.com",
            Password = "PrettyStrongPassword@123!"
        };
        
        var result = await _userService.AddUserAsync(createUserStub);
        
        Assert.NotNull(result);
        Assert.Equal("Test Test", result.Name);
        Assert.Equal("test@email.com", result.Email);
        Assert.IsType<Guid>(result.Id);
    }
    
    [Fact]
    public async Task CreateUser_UserAlreadyExists_ShouldTrowConflictException()
    {
        var createUserStub = new CreateUserDto
        {
            Name = "Test Test",
            Email = "test@email.com",
            Password = "PrettyStrongPassword@123!"
        };
        
        _emailHelperMock
            .Setup(r => r.EmailExists(createUserStub.Email))
            .ThrowsAsync(new ConflictException("User with that email already exist"));

        await Assert.ThrowsAsync<ConflictException>(() => 
            _userService.AddUserAsync(createUserStub));
        
        _userRepositoryMock
            .Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task PatchUserById_ValidUser_ShouldReturnNoContent()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Old Name",
            Email = "old@email.com",
        };

        var updateUserStub = new UpdateUserDto
        {
            Name = "New Name",
            Email = "new@email.com",
        };
        
        _userRepositoryMock
            .Setup(r => r.FindUserByIdAsync(user.Id.ToString()))
            .ReturnsAsync(user);

        await _userService.PatchUserByIdAsync(updateUserStub, user.Id.ToString());
        
        Assert.Equal("New Name", user.Name);
        Assert.Equal("new@email.com", user.Email);
        
        _userRepositoryMock
            .Verify(r => r.FindUserByIdAsync(user.Id.ToString()), Times.Once);
    }
    
    [Fact]
    public async Task PatchUserById_InvalidUser_ShouldThrowNotFoundException()
    {
        var userId = Guid.NewGuid().ToString();

        _userRepositoryMock
            .Setup(r => r.FindUserByIdAsync(userId))
            .ReturnsAsync((User)null!);

        await Assert.ThrowsAsync<NotFoundException>(() => 
            _userService.PatchUserByIdAsync(new UpdateUserDto(), userId));
    }
}