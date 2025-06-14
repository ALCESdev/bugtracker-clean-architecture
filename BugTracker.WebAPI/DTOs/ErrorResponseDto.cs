namespace BugTracker.WebAPI.DTOs;

public class ErrorResponseDto
{
    public string Message { get; set; } = "Ha ocurrido un error inesperado.";
    public string? Details { get; set; }
    public string? TraceId { get; set; }
    public int StatusCode { get; set; }
}