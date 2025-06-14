using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Interfaces;

public interface IBugTrackerDbContext
{
    DbSet<Project> Projects { get; }
    DbSet<Issue> Issues { get; }
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}