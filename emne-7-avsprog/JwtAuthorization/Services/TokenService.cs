﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthorization.Configuration;
using JwtAuthorization.Models;
using JwtAuthorization.Models.Interfaces;
using JwtAuthorization.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace JwtAuthorization.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    
    public async Task<IIdentityUser> LoginAsync(string username, string password)
    {
        await Task.Delay(10);
        return new IdentityUser
        {
            UserName = username,
            Id = Guid.NewGuid(),
            Roles =
            [
                new Role(){Id = Guid.NewGuid(), Name = "Admin"},
                new Role(){Id = Guid.NewGuid(), Name = "Developer"},
            ]
        };
    }

    public async Task<string?> GenerateJwtTokenAsync(IIdentityUser user)
    {
        await Task.Delay(10);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
            new Claim(ClaimTypes.Name, user.UserName!)
        ];
        
        // legg til roller
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name!));
        }
        var secrets = Encoding.UTF8.GetBytes(_jwtOptions.Value.Key!);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtOptions.Value.Issuer,
            Audience = _jwtOptions.Value.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secrets), 
                SecurityAlgorithms.HmacSha256)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}