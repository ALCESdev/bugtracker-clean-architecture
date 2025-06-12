using BugTracker.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project?>
{
    private readonly IBugTrackerDbContext _context;

    public GetProjectByIdQueryHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
    }
}