using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using FinTrack.Application.Operation.Authorization;
using FinTrack.Application.Security;
using FinTrack.Application.Security.Authorization;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Utils.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        //Services
        services.AddScoped<SecurityService>();
        
        //Mappers
        services.AddScoped<SecurityMapper>();
        
        // Authorization
        services.AddSecurityAuthorization();
        services.AddOperationAuthorization();
        
        // Validation Services
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
