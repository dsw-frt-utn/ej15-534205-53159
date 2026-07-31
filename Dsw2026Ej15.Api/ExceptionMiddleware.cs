using System.Text.Json;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Api.Middlewares;

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
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var response = new { errorCode = 400, message = ex.Message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new { errorCode = 500, message = "Ocurrió un error inesperado en el servidor." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}