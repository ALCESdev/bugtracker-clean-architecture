using BugTracker.Application.Projects.Commands.CreateProject;
using BugTracker.Application.Projects.Commands.DeleteProject;
using BugTracker.Application.Projects.Commands.UpdateProject;
using BugTracker.Application.Projects.DTOs;
using BugTracker.Application.Projects.Queries.GetAllProjects;
using BugTracker.Application.Projects.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        Guid projectId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = projectId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        ProjectDto? project = await _mediator.Send(new GetProjectByIdQuery(id));

        if (project is null)
            return NotFound();

        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<ProjectDto> projects = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(projects);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool result = await _mediator.Send(new DeleteProjectCommand(id));

        if (!result)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectCommand command)
    {
        if (id != command.Id)
            return BadRequest("Project ID mismatch.");

        bool result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return Ok(result);
    }
}