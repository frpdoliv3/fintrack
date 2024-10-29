using FinTrack.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FinTrack.Persistence.Models;

public class MigrationService
{
    public static async Task ApplyMigrationsAndSeed(IServiceProvider serviceProvider, string resourceBasePath)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationService>>();
        logger.LogInformation("Migration Service is starting.");
        var dbContext = scope.ServiceProvider.GetRequiredService<FinTrackDbContext>(); 

        await dbContext.Database.MigrateAsync();
        
        await new SeedData(serviceProvider, "Resources").Initialize();
    }

    public static async Task DeleteDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationService>>();
        logger.LogInformation("Migration Service is deleting the database.");
        var dbContext = scope.ServiceProvider.GetRequiredService<FinTrackDbContext>();
        await dbContext.Database.EnsureDeletedAsync();
    }
}
