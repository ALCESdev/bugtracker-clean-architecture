using BugTracker.Domain.Enums;
using MediatR;

namespace BugTracker.Application.Issues.Commands.CreateIssueCommand;

public record CreateIssueCommand(
    Guid ProjectId,
    Guid ReporterId,
    Guid AssigneeId,
    string Title,
    string Description,
    IssuePriority Priority = IssuePriority.Medium,
    IssueStatus Status = IssueStatus.ToDo
) : IRequest<Guid>;