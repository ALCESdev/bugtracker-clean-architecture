using BugTracker.Application.Interfaces;
using BugTracker.Application.Projects.DTOs;
using BugTracker.Application.Projects.Queries.GetAllProjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IBugTrackerDbContext _context;

    public GetAllProjectsQueryHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}