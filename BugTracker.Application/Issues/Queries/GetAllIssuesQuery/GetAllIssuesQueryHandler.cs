using BugTracker.Application.Interfaces;
using BugTracker.Application.Issues.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Issues.Queries.GetAllIssuesQuery;

public class GetAllIssuesQueryHandler : IRequestHandler<GetAllIssuesQuery, IEnumerable<IssueDto>>
{
    private readonly IBugTrackerDbContext _context;

    public GetAllIssuesQueryHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IssueDto>> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Issues
            .Select(issue => new IssueDto
            {
                Id = issue.Id,
                ProjectId = issue.ProjectId,
                ReporterId = issue.ReporterId,
                AssigneeId = issue.AssigneeId,
                Title = issue.Title,
                Description = issue.Description,
                Priority = issue.Priority,
                Status = issue.Status,
                CreatedAt = issue.CreatedAt,
                UpdatedAt = issue.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}