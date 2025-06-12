using BugTracker.Application.Projects.DTOs;
using MediatR;

namespace BugTracker.Application.Projects.Queries.GetAllProjects;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>;