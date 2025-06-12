using MediatR;

namespace BugTracker.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : IRequest<bool>;