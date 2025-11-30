using CashPilot.Application.Configuration;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.Entities;
using Microsoft.Extensions.Options;

namespace CashPilot.Application.Tests.Services;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;
    private readonly User _userStub;

    public TokenServiceTests()
    {
        var jwtSettings = new JwtSettings
        {
            SecretKey = "my_super_secret-my_super_secretmy_super_secretmy_super_secretmy_super_secretmy_super_secretmy_super_secretmy_super_secretmy_super_secret",
            Audience = "my_audience",
            Issuer = "my_issuer",
            ExpirationMinutes = 60
        };
        
        var options = Options.Create(jwtSettings);

        _tokenService = new TokenService(options);
        _userStub = new User
        {
            Id = Guid.NewGuid(), 
            Email = "test@email.com"
        };
    }

    [Fact]
    public void GenerateToken_ShouldReturnJwt()
    {
        var token = _tokenService.GenerateToken(_userStub.Id.ToString(), _userStub.Email);
        
        Assert.False(string.IsNullOrEmpty(token));
    }
    
    [Fact]
    public void GenerateValidationToken_ShouldReturnJwt()
    {
        var token = _tokenService.GenerateVerificationToken(_userStub.Email, 5);
        
        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void ValidateVerificationToken_ValidToken_ShouldReturnTrue()
    {
       var token = _tokenService.GenerateVerificationToken(_userStub.Email, 5);
    
       var isValid = _tokenService.ValidateVerificationToken(token);
       
       Assert.True(isValid);
    }
    
    [Fact]
    public void ValidateVerificationToken_InvalidToken_ShouldReturnFalse()
    {
        const string token = "random Token";
    
        var isValid = _tokenService.ValidateVerificationToken(token);
       
        Assert.False(isValid);
    }
}