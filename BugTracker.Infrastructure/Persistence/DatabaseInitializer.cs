using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BugTracker.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task ResetAsync(IServiceProvider services)
    {
        var env = services.GetRequiredService<IHostEnvironment>();
        if (!env.IsDevelopment())
            throw new InvalidOperationException("Database reset is only allowed in Development environment.");

        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseInitializer");
        logger.LogWarning("Reseteando base de datos...");

        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BugTrackerDbContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();

        DataSeeder.Seed(context);

        logger.LogInformation("Base de datos reinicializada correctamente.");
    }
}