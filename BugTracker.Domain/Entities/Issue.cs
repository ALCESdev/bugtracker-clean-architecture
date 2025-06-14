using BugTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Domain.Entities;

public class Issue
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public IssueStatus Status { get; set; } = IssueStatus.ToDo;
    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    [Required]
    public Guid AssigneeId { get; set; }
    public User? Assignee { get; set; }

    [Required]
    public Guid ReporterId { get; set; }
    public User? Reporter { get; set; }

    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}