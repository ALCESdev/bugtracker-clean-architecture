using BugTracker.Application.Projects.Commands.CreateProject;
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
}