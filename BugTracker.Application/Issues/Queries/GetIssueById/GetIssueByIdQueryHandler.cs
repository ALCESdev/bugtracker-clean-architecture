using BugTracker.Application.Interfaces;
using BugTracker.Application.Issues.DTOs;
using MediatR;

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
            return null;
        
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
