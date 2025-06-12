using BugTracker.Application.Issues.DTOs;
using MediatR;

namespace BugTracker.Application.Issues.Queries.GetIssueById;

public record GetIssueByIdQuery(Guid Id) : IRequest<IssueDto?>;