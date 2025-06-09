using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BugTrackerDbContext>
{
    public BugTrackerDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddUserSecrets("be6b0c21-3e57-4d38-a465-9e9398cd8656")
            .Build();

        DbContextOptionsBuilder<BugTrackerDbContext> optionsBuilder = new();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new BugTrackerDbContext(optionsBuilder.Options);
    }
}