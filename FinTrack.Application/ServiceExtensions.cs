using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

namespace FinTrack.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        //Services
        services.AddScoped<SecurityService>();
        
        //Mappers
        services.AddScoped<CreateSecurityMapper>();
        
        // Validation Services
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationRulesToSwagger();
    }
}
