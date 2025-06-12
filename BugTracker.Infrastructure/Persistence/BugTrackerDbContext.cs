using BugTracker.Application.Interfaces;
using BugTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Infrastructure.Persistence;

public class BugTrackerDbContext : DbContext, IBugTrackerDbContext
{
    public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Issue> Issues => Set<Issue>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.ReportedIssues)
            .WithOne(i => i.Reporter)
            .HasForeignKey(i => i.ReporterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.AssignedIssues)
            .WithOne(i => i.Assignee)
            .HasForeignKey(i => i.AssigneeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Issues)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}