using System.Text.Json;
using Domain.Exceptions;

namespace Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await WriteErrorResponse(context, 404, ex.Message);
        }
        catch (ValidationDomainException ex)
        {
            await WriteErrorResponse(context, 400, ex.Message);
        }
        catch (Exception)
        {
            await WriteErrorResponse(context, 500, "Internal server error");
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = message }));
    }
}
