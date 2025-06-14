using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using BugTracker.Application.Issues.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Issues.Queries.GetIssueById;

public class GetIssueByIdQueryHandler : IRequestHandler<GetIssueByIdQuery, IssueDto?>
{
    private readonly IBugTrackerDbContext _context;

    public GetIssueByIdQueryHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IssueDto?> Handle(GetIssueByIdQuery request, CancellationToken cancellationToken)
    {
        Issue? issue = await _context.Issues.FindAsync([request.Id], cancellationToken);
        
        if (issue is null)
            throw new NotFoundException($"Issue with ID {request.Id} not found.");

        if (!await _context.Projects.AnyAsync(p => p.Id == issue.ProjectId, cancellationToken))
            throw new NotFoundException($"Project with ID {issue.ProjectId} not found.");

        if (!await _context.Users.AnyAsync(u => u.Id == issue.ReporterId, cancellationToken))
            throw new NotFoundException($"Reporter with ID {issue.ReporterId} not found.");

        if (!await _context.Users.AnyAsync(u => u.Id == issue.AssigneeId, cancellationToken))
            throw new NotFoundException($"Assignee with ID {issue.AssigneeId} not found.");

        return new IssueDto
        {
            Id = issue.Id,
            ProjectId = issue.ProjectId,
            ReporterId = issue.ReporterId,
            AssigneeId = issue.AssigneeId,
            Title = issue.Title,
            Description = issue.Description,
            CreatedAt = issue.CreatedAt,
            UpdatedAt = issue.UpdatedAt,
            Priority = issue.Priority,
            Status = issue.Status
        };
    }
}
