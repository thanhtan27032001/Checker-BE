using System.Net;
using System.Text.Json;
using GadenCheckIn.API.Common;
using GadenCheckIn.API.Common.Exceptions;

namespace GadenCheckIn.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception at {Path}", context.Request.Path);
            await ExceptionHandlerAsync(context, ex);
        }
    }

    public static Task ExceptionHandlerAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            BusinessRuleException businessRuleException => (HttpStatusCode.BadRequest, businessRuleException.Message),
            NotFoundException notFoundException => (HttpStatusCode.NotFound, notFoundException.Message),
            _ => (HttpStatusCode.InternalServerError, "Something went wrong")
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(
            ApiResponse.Fail(message),
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        );
        
        return context.Response.WriteAsync(json);
    }
}