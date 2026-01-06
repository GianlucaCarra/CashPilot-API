using AutoMapper;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Mapping;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace CashPilot.Application.Tests.Common;

public abstract class TestBase
{
    protected IMapper Mapper { get; }
    
    protected TestBase()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserProfile>();
        }, NullLoggerFactory.Instance);

        config.AssertConfigurationIsValid();

        Mapper = config.CreateMapper();
    }
}