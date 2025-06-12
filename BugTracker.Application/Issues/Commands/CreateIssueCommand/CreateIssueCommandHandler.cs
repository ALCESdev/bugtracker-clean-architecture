using BugTracker.Application.Interfaces;
using MediatR;

namespace BugTracker.Application.Issues.Commands.CreateIssueCommand;

public record CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Guid>
{
    private readonly IBugTrackerDbContext _context;

    public CreateIssueCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        Issue issue = new()
        {
            ProjectId = request.ProjectId,
            ReporterId = request.ReporterId,
            AssigneeId = request.AssigneeId,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        _context.Issues.Add(issue);
        await _context.SaveChangesAsync(cancellationToken);
        return issue.Id;
    }
}
