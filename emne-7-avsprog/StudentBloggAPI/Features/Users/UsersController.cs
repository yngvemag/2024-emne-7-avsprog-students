using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.Features.Users;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    // henter logger fra DI-Container, gjøres alltid i konstruktøren 
    public UsersController(ILogger<UsersController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    [HttpGet("hello", Name = "SayHello")]
    public async Task<ActionResult<string>> SayHello()
    {
        _logger.LogInformation("Hello from API");
        await Task.Delay(20);
        return Ok("hello from API");
    }
    
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync()
    {
        var userDtos = await _userService.GetAllAsync();
        return userDtos is null
            ? BadRequest("No users found!")
            : Ok(userDtos);
    }

}