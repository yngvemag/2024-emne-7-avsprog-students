using JwtAuthorization.Models;

namespace JwtAuthorization.Services.Interfaces;

public interface ITokenService
{
    Task<IIdentityUser> LoginAsync(string username, string password);
    Task<string?> GenerateJwtTokenAsync(IIdentityUser user);
    
    (string? userId, IEnumerable<string>? roles) ValidateAccessToken(string accessToken);
}