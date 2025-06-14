using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using BugTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Issues.Commands.CreateIssueCommand;

public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Guid>
{
    private readonly IBugTrackerDbContext _context;

    public CreateIssueCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Projects.AnyAsync(p => p.Id == request.ProjectId, cancellationToken))
            throw new NotFoundException($"El proyecto con ID '{request.ProjectId}' no existe.");

        if (!await _context.Users.AnyAsync(u => u.Id == request.ReporterId, cancellationToken))
            throw new NotFoundException($"El usuario con ID '{request.ReporterId}' (reporter) no existe.");

        if (!await _context.Users.AnyAsync(u => u.Id == request.AssigneeId, cancellationToken))
            throw new NotFoundException($"El usuario con ID '{request.AssigneeId}' (assignee) no existe.");

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