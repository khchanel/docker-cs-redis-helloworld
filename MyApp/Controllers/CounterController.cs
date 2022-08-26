using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace MyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly ILogger<CounterController> _logger;
    private readonly IConnectionMultiplexer _redis;

    public CounterController(ILogger<CounterController> logger, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _redis = redis;
    }

    [HttpGet]
    public string Get()
    {
        var db = _redis.GetDatabase();
        var hits = db.StringIncrement("hits");

        return $"Hello! you have visited {hits} times";
    }
}
