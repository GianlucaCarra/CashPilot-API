using CashPilot.Application.Interfaces.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Interfaces.Services.Caching;
using CashPilot.Application.Services;
using CashPilot.Application.Tests.Common;
using CashPilot.Domain.DTOs.Users.Request;
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