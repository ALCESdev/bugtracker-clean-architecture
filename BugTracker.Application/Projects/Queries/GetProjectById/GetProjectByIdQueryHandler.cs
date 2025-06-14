using BugTracker.Application.Common.Exceptions;
using BugTracker.Application.Interfaces;
using BugTracker.Application.Projects.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IBugTrackerDbContext _context;

    public GetProjectByIdQueryHandler(IBugTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        Project? project = await _context.Projects.FindAsync([request.Id], cancellationToken);

        if (project is null)
            throw new NotFoundException($"Project with ID {request.Id} not found.");

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
    }
}