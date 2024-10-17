using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using FinTrack.Application.Country;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

namespace FinTrack.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<CountryService>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationRulesToSwagger();
        }
    }
}
