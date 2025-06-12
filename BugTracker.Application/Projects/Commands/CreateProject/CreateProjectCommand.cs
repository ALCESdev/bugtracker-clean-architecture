using MediatR;

namespace BugTracker.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(string Name, string Description) : IRequest<Guid>;