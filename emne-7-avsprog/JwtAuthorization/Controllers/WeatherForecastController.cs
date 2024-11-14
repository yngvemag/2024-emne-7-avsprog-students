using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthorization.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    
    
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("roles", Name = "GetWeatherForecastById")]
    public ActionResult<IEnumerable<string>> GetUserAndRoles()
    {
        List<string> results = [];
        var roles = HttpContext.User.Claims
            .Where(x => x.Type == ClaimTypes.Role)
            .Select(x => x.Value);
        
        var user = HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;
        
        if (!string.IsNullOrEmpty(user)) 
            results.Add(user);
        results.AddRange(roles);
        // if (HttpContext.Items.ContainsKey("UserId"))
        // {
        //     if (HttpContext.Items["UserId"] is string userId)
        //         results.Add(userId);
        // }
        //
        // if (HttpContext.Items.ContainsKey("Roles"))
        // {
        //     if (HttpContext.Items["Roles"] is IEnumerable<string> roles)
        //         results.AddRange(roles);
        // }
        
        return Ok(results);
        
    }
}