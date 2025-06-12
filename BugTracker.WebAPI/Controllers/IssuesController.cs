using BugTracker.Application.Issues.Commands.CreateIssueCommand;
using BugTracker.Application.Issues.Commands.DeleteIssueCommand;
using BugTracker.Application.Issues.Commands.UpdateIssueCommand;
using BugTracker.Application.Issues.DTOs;
using BugTracker.Application.Issues.Queries.GetAllIssuesQuery;
using BugTracker.Application.Issues.Queries.GetIssueById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : Controller
{
    private readonly IMediator _mediator;

    public IssuesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] CreateIssueCommand command)
    {
        Guid issueId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = issueId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        IssueDto? issue = await _mediator.Send(new GetIssueByIdQuery(id));

        if (issue is null)
            return NotFound();

        return Ok(issue);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<IssueDto> issues = await _mediator.Send(new GetAllIssuesQuery());
        return Ok(issues);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool result = await _mediator.Send(new DeleteIssueCommand(id));

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIssueCommand command)
    {
        if (id != command.Id)
            return BadRequest("Issue ID mismatch.");

        bool result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}