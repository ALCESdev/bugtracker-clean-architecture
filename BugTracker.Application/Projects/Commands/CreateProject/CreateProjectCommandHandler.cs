using BugTracker.Application.Interfaces;
using MediatR;

namespace BugTracker.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IBugTrackerDbContext _context;
    public CreateProjectCommandHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        Project project = new()
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}