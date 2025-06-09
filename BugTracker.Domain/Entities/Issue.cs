using BugTracker.Domain.Entities;
using BugTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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

    public Guid AssigneeId { get; set; }
    public User? Assignee { get; set; }

    public Guid ReporterId { get; set; }
    public User? Reporter { get; set; }

    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}