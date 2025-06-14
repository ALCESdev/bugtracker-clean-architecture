using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using BugTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            throw new NotFoundException($"El issue con ID '{request.Id}' no existe.");

        if (!await _context.Projects.AnyAsync(p => p.Id == request.ProjectId, cancellationToken))
            throw new NotFoundException($"El proyecto con ID '{request.ProjectId}' no existe.");

        if (!await _context.Users.AnyAsync(u => u.Id == request.ReporterId, cancellationToken))
            throw new NotFoundException($"El usuario con ID '{request.ReporterId}' (reporter) no existe.");

        if (!await _context.Users.AnyAsync(u => u.Id == request.AssigneeId, cancellationToken))
            throw new NotFoundException($"El usuario con ID '{request.AssigneeId}' (assignee) no existe.");

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