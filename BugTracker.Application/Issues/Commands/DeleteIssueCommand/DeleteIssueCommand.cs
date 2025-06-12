using BugTracker.Domain.Enums;
using MediatR;

namespace BugTracker.Application.Issues.Commands.DeleteIssueCommand;

public record DeleteIssueCommand(Guid Id) : IRequest<bool>;