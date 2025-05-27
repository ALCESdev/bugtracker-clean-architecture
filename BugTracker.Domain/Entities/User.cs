using BugTracker.Domain.Enums;

namespace BugTracker.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole UserRole { get; set; }
    public List<Issue> ReportedIssues { get; set; } = [];
    public List<Issue> AssignedIssues { get; set; } = [];
}