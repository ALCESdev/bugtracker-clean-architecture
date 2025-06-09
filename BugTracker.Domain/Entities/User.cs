using BugTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    public UserRole UserRole { get; set; }

    public List<Issue> ReportedIssues { get; set; } = [];
    public List<Issue> AssignedIssues { get; set; } = [];
}