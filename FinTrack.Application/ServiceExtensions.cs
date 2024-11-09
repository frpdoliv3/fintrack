using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;

namespace FinTrack.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        //Services
        services.AddScoped<SecurityService>();
        
        //Mappers
        services.AddScoped<SecurityMapper>();
        
        // Validation Services
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
