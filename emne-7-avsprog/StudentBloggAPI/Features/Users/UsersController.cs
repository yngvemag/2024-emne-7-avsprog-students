using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.Features.Users;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    // henter logger fra DI-Container, gjøres alltid i konstruktøren 
    public UsersController(
        ILogger<UsersController> logger, 
        IUserService userService)
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
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync(
        [FromQuery] UserSearchParams? searchParams,
        [FromQuery] int pageNr = 1, 
        [FromQuery] int pageSize = 10)
    {
        if (searchParams?.FirstName is null &&
            searchParams?.LastName is null &&
            searchParams?.Email is null &&
            searchParams?.UserName is null)
        {
            var userDtos = await _userService.GetPagedAsync(pageNr,pageSize);
            return Ok(userDtos);
        }
        return Ok(await _userService.FindAsync(searchParams));
    }
    
    [HttpGet("{id}", Name = "GetUserByIdAsync")]
    public async Task<ActionResult<UserDTO>> GetUserByIdAsync(Guid id)
    {
        var userDto = await _userService.GetByIdAsync(id);
        return userDto is null
            ? BadRequest("User not found")
            : Ok(userDto);
    }
    
    //  dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8
    // https://localhost:54634/api/v1/users/register
    [HttpPost("register",Name = "RegisterUserAsync")]
    public async Task<ActionResult<UserDTO>> RegisterUserAsync(UserRegistrationDTO dto)
    {
        var user = await _userService.RegisterAsync(dto);
        return user is null
            ? BadRequest("Failed to register new user")
            : Ok(user);
    }

    [HttpDelete("{id}", Name = "DeleteUserAsync")]
    public async Task<ActionResult<bool>> DeleteUserAsync(Guid id)
    {
        _logger.LogDebug("Deleting user with id: ({UserID})", id);
        var result = await _userService.DeleteByIdAsync(id);
        
        return result
            ? Ok(result)
            : BadRequest($"Failed to delete user with id={id}");
    }
    

}