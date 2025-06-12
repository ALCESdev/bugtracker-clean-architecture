using BugTracker.Application.Interfaces;
using MediatR;

namespace BugTracker.Application.Issues.Commands.DeleteIssueCommand;

public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, bool>
{
    private readonly IBugTrackerDbContext _context;

    public DeleteIssueCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        Issue? issue = await _context.Issues.FindAsync([ request.Id ], cancellationToken);

        if (issue is null)
            return false;

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}