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

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? FullName { get; set; }

    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    [MaxLength(250)]
    public string? Address { get; set; }

    public UserRole UserRole { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public List<Issue> ReportedIssues { get; set; } = [];
    public List<Issue> AssignedIssues { get; set; } = [];
}