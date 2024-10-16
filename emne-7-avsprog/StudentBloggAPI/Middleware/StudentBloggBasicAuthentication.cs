﻿using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using StudentBloggAPI.Configurations;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.Middleware;

public class StudentBloggBasicAuthentication : IMiddleware
{
    private readonly ILogger<StudentBloggBasicAuthentication> _logger;
    private readonly IUserService _userService;
    private readonly List<Regex> _excludePatterns;

    public StudentBloggBasicAuthentication(
        ILogger<StudentBloggBasicAuthentication> logger,
        IUserService userService,
        IOptions<BasicAuthenticationOptions> options)
    {
        _logger = logger;
        _userService = userService;
        
        _excludePatterns = options.Value.ExcludePatterns
            .Select(p => new Regex(p)).ToList();
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        foreach (var regex in _excludePatterns)
        {
            if (regex.IsMatch(context.Request.Path))
            {
                await next(context);
                return;
            }   
        }
        
        string authHeader = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(authHeader))
        {
            _logger.LogWarning("Authentication header is empty");
            throw new UnauthorizedAccessException("Authentication header is empty");
        }
        
        // sjekk om authorization header starter med "Basic"
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("Authentication header is incorrect");
            throw new UnauthorizedAccessException("Authentication header is incorrect");
        }
        
        SplitString(authHeader, " ", out string basic, out string base64String);
        if (string.IsNullOrWhiteSpace(base64String) || string.IsNullOrWhiteSpace(basic))
        {
            _logger.LogWarning("Authentication header is empty");
            throw new UnauthorizedAccessException("Authentication header is empty");
        }
        
        // Decode base64-string -> username og passord
        string userName, password;
        try
        {
            // username:password
            string userNamePassword = ExtractBase64String(base64String);
            SplitString(userNamePassword, 
                ":", 
                out userName, 
                out password);

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("Missing user name and/or password");
                throw new UnauthorizedAccessException("Missing user name and/or password");
            }
            
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Authentication header is incorrect");
            throw new UnauthorizedAccessException("Authentication header is incorrect", e);
        }
        
        // nå har vi username og passord !!
        Guid userId = await _userService.AuthenticateUserAsync(userName, password);
        if (userId == Guid.Empty)
        {
            _logger.LogWarning("Username or password is incorrect");
            throw new UnauthorizedAccessException("Username or password is incorrect");
        }
        
        // Kommer vi hit har vi en gyldig bruker! Vi får bruk for denne id når vi authorize
        context.Items["UserId"] = userId.ToString();
        
        
        // Går videre til neste middleware !!
        await next(context);
    }

    private string ExtractBase64String(string base64String)
    {
        var base64Bytes = Convert.FromBase64String(base64String);
        var userNamePassword = Encoding.UTF8.GetString(base64Bytes);
        return userNamePassword;
    }

    private void SplitString(string authHeader, string separator, out string left, out string right)
    {
        left = right = string.Empty; 
        var arr = authHeader.Split(separator);
        if (arr is [var a, var b])
        {
            left = a;
            right = b;
        }
    }
}