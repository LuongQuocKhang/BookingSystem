using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Stay.Api.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StayController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<StayController> _logger;

    public StayController(ILogger<StayController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<int> Get()
    {
        return Enumerable.Range(1, 5)
        .ToArray();
    }
}
