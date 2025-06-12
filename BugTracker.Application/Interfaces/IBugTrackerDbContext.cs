using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Interfaces;

public interface IBugTrackerDbContext
{
    DbSet<Project> Projects { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}