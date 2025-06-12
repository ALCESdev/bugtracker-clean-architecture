using BugTracker.Application.Issues.DTOs;
using MediatR;

namespace BugTracker.Application.Issues.Queries.GetAllIssuesQuery;

public record GetAllIssuesQuery() : IRequest<IEnumerable<IssueDto>>;