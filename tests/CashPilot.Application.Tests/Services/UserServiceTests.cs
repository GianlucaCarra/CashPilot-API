using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Application.Services.Caching;
using CashPilot.Domain.DTOs.Users.Request;
using Moq;

namespace CashPilot.Application.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly EmailHelper _emailHelperMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<VerificationService> _verificationServiceMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<ResetPasswordAttemptService> _resetPasswordAttemptServiceMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _emailHelperMock = new EmailHelper(_userRepositoryMock.Object);
        _mapperMock = new Mock<IMapper>();
        _verificationServiceMock = new Mock<VerificationService>();
        _tokenServiceMock = new Mock<ITokenService>();
        _resetPasswordAttemptServiceMock = new Mock<ResetPasswordAttemptService>();
        
        _userService = new UserService(
            _userRepositoryMock.Object,
            _emailHelperMock,
            _mapperMock.Object,
            _verificationServiceMock.Object,
            _tokenServiceMock.Object,
            _resetPasswordAttemptServiceMock.Object
            );
    }

    [Fact]
    public async Task CreateUser_ShouldReturnUserDto()
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
}