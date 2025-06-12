using BugTracker.Application.Interfaces;
using MediatR;

namespace BugTracker.Application.Issues.Commands.UpdateIssueCommand;

public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, bool>
{
    private readonly IBugTrackerDbContext _context;

    public UpdateIssueCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        Issue? issue = await _context.Issues.FindAsync([request.Id], cancellationToken);

        if (issue is null)
            return false;
        
        issue.Title = request.Title;
        issue.Description = request.Description;
        issue.Status = request.Status;
        issue.Priority = request.Priority;
        issue.AssigneeId = request.AssigneeId;
        issue.ReporterId = request.ReporterId;
        issue.ProjectId = request.ProjectId;
        issue.UpdatedAt = DateTime.UtcNow;

        _context.Issues.Update(issue);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}