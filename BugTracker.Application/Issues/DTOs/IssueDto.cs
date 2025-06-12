using BugTracker.Domain.Enums;

namespace BugTracker.Application.Issues.DTOs;

public class IssueDto
{
    public Guid Id { get; init; }
    public Guid ProjectId { get; init; }
    public Guid ReporterId { get; init; }
    public Guid AssigneeId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public IssuePriority Priority { get; init; } = IssuePriority.Medium;
    public IssueStatus Status { get; init; } = IssueStatus.ToDo;
}