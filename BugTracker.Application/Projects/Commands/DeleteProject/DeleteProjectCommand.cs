using MediatR;

namespace BugTracker.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid ProjectId) : IRequest<bool>;