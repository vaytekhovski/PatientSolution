using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate Next;
    private readonly ILogger<ExceptionHandlingMiddleware> Logger;

    private static readonly Dictionary<Type, int> ExceptionStatusCodes = new()
    {
        { typeof(ValidationException), StatusCodes.Status400BadRequest },
        { typeof(NotFoundException), StatusCodes.Status404NotFound },
        { typeof(DatabaseException), StatusCodes.Status500InternalServerError }
    };

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        response.StatusCode = ExceptionStatusCodes.GetValueOrDefault(ex.GetType(), StatusCodes.Status500InternalServerError);

        if (response.StatusCode == StatusCodes.Status500InternalServerError)
            Logger.LogError(ex, "Unexpected error occurred");

        await response.WriteAsync(JsonSerializer.Serialize(new
        {
            error = ex.Message
        }));
    }
}
