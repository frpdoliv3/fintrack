using Application;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace FinTrack.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<CountryService>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation(opts =>
            {
                opts.DisableBuiltInModelValidation = true;
            });
        }
    }
}
