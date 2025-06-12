using MediatR;

namespace BugTracker.Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(Guid Id) : IRequest<Project?>;