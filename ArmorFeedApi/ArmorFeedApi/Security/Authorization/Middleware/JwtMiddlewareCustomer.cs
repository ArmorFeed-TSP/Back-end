using System.Security.Claims;
using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Customers.Domain.Services;
using ArmorFeedApi.Security.Authorization.Handlers.Interfaces;
using ArmorFeedApi.Security.Authorization.Settings;
using ArmorFeedApi.Security.Domain.Services;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ArmorFeedApi.Security.Authorization.Middleware;

public class JwtMiddlewareCustomer
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddlewareCustomer(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, ICustomerService userService, IJwtHandler<Customer> handler)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);
        if (userId != null)
        {
            // On success JWT validation, attach user to context
            context.Items["Customer"] = await userService.GetByIdAsync(userId.Value);
        }
        await _next(context);
    }
    private async Task<bool> IsGoogleUserAsync(HttpContext context)
    {
        var authenticateResult = await context.AuthenticateAsync(GoogleOpenIdConnectDefaults.AuthenticationScheme);
        return authenticateResult.Succeeded;
    }

    private async Task<string> GetGoogleEmailAsync(HttpContext context)
    {
        var authenticateResult = await context.AuthenticateAsync(GoogleOpenIdConnectDefaults.AuthenticationScheme);
        if (authenticateResult.Succeeded)
        {
            return authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
        }
        return null;
    }

    private async Task<string> GetGoogleNameAsync(HttpContext context)
    {
        var authenticateResult = await context.AuthenticateAsync(GoogleOpenIdConnectDefaults.AuthenticationScheme);
        if (authenticateResult.Succeeded)
        {
            return authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
        }
        return null;
    }

}