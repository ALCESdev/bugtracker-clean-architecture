using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using BugTracker.Domain.Entities;
using MediatR;

namespace BugTracker.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IBugTrackerDbContext _context;

    public DeleteProjectCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _context.Projects.FindAsync([request.Id], cancellationToken);
        
        if (project is null)
            throw new NotFoundException($"El proyecto con ID '{request.Id}' no existe.");

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}