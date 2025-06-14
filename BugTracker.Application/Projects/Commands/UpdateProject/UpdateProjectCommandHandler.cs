using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using BugTracker.Domain.Entities;
using MediatR;

namespace BugTracker.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, bool>
{
    private readonly IBugTrackerDbContext _context;

    public UpdateProjectCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        Project? project = await _context.Projects.FindAsync([request.Id], cancellationToken);
        
        if (project is null)
            throw new NotFoundException($"El proyecto con ID '{request.Id}' no existe.");

        project.Name = request.Name;
        project.Description = request.Description;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}