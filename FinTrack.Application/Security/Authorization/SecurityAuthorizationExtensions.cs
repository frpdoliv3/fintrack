using FinTrack.Application.Utils.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FinTrack.Application.Security.Authorization;

public static class SecurityAuthorizationExtensions
{
    public static void AddSecurityAuthorization(this IServiceCollection services)
    {
        // ChangeSecurityRequirement
        services
            .AddScoped<IAuthorizationHandler,
                SecurityAuthorRequirementHandler<SecurityAuthorization.ChangeSecurityRequirement>>();
        
        // ViewSecurityRequirement
        services
            .AddScoped<IAuthorizationHandler, AdminRequirementHandler<SecurityAuthorization.ViewSecurityRequirement>>();
        services
            .AddScoped<IAuthorizationHandler,
                SecurityAuthorRequirementHandler<SecurityAuthorization.ViewSecurityRequirement>>();
    }
}