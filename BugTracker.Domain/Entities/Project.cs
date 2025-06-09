using BugTracker.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Project
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Issue> Issues { get; set; } = [];
}