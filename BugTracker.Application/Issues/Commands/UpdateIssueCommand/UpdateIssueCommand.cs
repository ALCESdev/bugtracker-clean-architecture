using BugTracker.Domain.Enums;
using MediatR;

namespace BugTracker.Application.Issues.Commands.UpdateIssueCommand;

public record UpdateIssueCommand(
    Guid Id,
    Guid ProjectId,
    Guid ReporterId,
    Guid AssigneeId,
    string Title,
    string Description,
    IssuePriority Priority = IssuePriority.Medium,
    IssueStatus Status = IssueStatus.ToDo
    ) : IRequest<bool>;