using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Context;
using FinTrack.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinTrack.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<FinTrackDbContext>(opts => opts.UseSqlServer(connectionString));

            // Put other DI services here
            services.AddScoped<ICountryRepository, EFCountryRepository>();
            services.AddScoped<ICurrencyRepository, EFCurrencyRepository>();
        }
    }
}
