using BugTracker.Application.Common.Exceptions;
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
        HttpResponse? response = context.Response;
        response.ContentType = "application/json";

        ErrorResponseDto errorResponse = new()
        {
            TraceId = context.TraceIdentifier,
        };

        switch (exception)
        {
            case NotFoundException nfEx:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = nfEx.Message;
                break;

            case BadRequestException brEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = brEx.Message;
                break;

            case ForbiddenException fEx:
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                errorResponse.Message = fEx.Message;
                break;

            case BusinessRuleValidationException bEx:
                response.StatusCode = (int)HttpStatusCode.UnprocessableEntity; // 422
                errorResponse.Message = bEx.Message;
                break;

            case ConflictException cEx:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Message = cEx.Message;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Ha ocurrido un error inesperado.";
                break;
        }

        errorResponse.StatusCode = response.StatusCode;
        if (env == "Development")
            errorResponse.Details = exception.Message;

        string json = JsonSerializer.Serialize(errorResponse);
        return response.WriteAsync(json);
    }

    #endregion
}
