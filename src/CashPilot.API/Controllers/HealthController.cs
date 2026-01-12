using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult Health()
    {
        return Ok("This app still works :)");
    }
} 