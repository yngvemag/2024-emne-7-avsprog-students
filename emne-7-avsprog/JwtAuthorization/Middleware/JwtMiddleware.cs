using JwtAuthorization.Services.Interfaces;

namespace JwtAuthorization.Middleware;

public class JwtMiddleware : IMiddleware
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(ITokenService tokenService, ILogger<JwtMiddleware> logger)
    {
        _tokenService = tokenService;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string? token = context.Request
            .Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null)
        {
            var (userId, roles) = _tokenService.ValidateAccessToken(token);
            _logger.LogInformation($"User: {userId}, Roles: {roles}");
            context.Items["UserId"] = userId;
            context.Items["Roles"] = roles;
        }
        
        await next(context);
    }
}