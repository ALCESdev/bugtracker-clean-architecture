using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            throw new NotFoundException($"El issue con ID '{request.Id}' no existe.");

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}