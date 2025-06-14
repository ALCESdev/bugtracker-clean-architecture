using BugTracker.WebAPI.DTOs;
using System.Net;
using System.Text.Json;

namespace BugTracker.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    #region VARIABLES

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    #endregion

    #region CONSTRUCTORS

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    #endregion

    #region METHODS

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió una excepción no controlada.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        ErrorResponseDto? errorResponse = new()
        {
            Message = "Ha ocurrido un error inesperado.",
            Details = env == "Development" ? exception.Message : null
        };

        string json = JsonSerializer.Serialize(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(json);
    }

    #endregion
}
