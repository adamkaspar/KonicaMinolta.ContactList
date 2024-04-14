using System.Net;
using KonicaMinolta.Shared.Domain.Exceptions;

namespace KonicaMinolta.ContactList.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        => (this.next, this.logger) = (next, logger);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var correlationId = Guid.NewGuid();

        logger.LogError(exception, $"CorrelationId: {correlationId}. An unexpected error occurred.");

        var response = exception switch
        {
            ApplicationException _ => new ExceptionDto (correlationId, HttpStatusCode.BadRequest, "Application exception occurred."),
            _ => new ExceptionDto (correlationId, HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;

        await context.Response.WriteAsJsonAsync(response);
    }
}
