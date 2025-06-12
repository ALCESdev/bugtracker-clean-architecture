using BugTracker.Application.Interfaces;
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
            return false;
        
        project.Name = request.Name;
        project.Description = request.Description;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}