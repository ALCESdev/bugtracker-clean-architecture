using BugTracker.Domain.Entities;
using BugTracker.Domain.Enums;

namespace BugTracker.Infrastructure.Persistence;

public static class DataSeeder
{
    public static void Seed(BugTrackerDbContext context)
    {
        if (!context.Users.Any())
        {
            User admin = new()
            {
                Username = "admin",
                Email = "admin@example.com",
                UserRole = UserRole.Admin
            };

            User dev = new()
            {
                Username = "developer",
                Email = "dev@example.com",
                UserRole = UserRole.Developer
            };

            context.Users.AddRange(admin, dev);
            context.SaveChanges();

            Project project = new()
            {
                Name = "BugTracker Core",
                Description = "Proyecto base del sistema de gestión de bugs"
            };

            context.Projects.Add(project);
            context.SaveChanges();

            Issue issue = new()
            {
                Title = "Primera prueba de bug",
                Description = "Este es un issue inicial de prueba",
                ReporterId = admin.Id,
                AssigneeId = dev.Id,
                ProjectId = project.Id,
                Status = IssueStatus.ToDo,
                Priority = IssuePriority.Medium
            };

            context.Issues.Add(issue);
            context.SaveChanges();
        }
    }
}