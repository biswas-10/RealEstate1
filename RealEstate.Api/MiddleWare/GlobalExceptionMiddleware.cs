using System.Text.Json;
using RealEstate.Api.Contracts;
using RealEstate.Api.Middleware;

namespace RealEstate.Api.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IExceptionMapper _exceptionMapper;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        IExceptionMapper exceptionMapper)
    {
        _next = next;
        _exceptionMapper = exceptionMapper;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        var result = _exceptionMapper.Map(exception);

        context.Response.StatusCode = result.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(result.Response);

        await context.Response.WriteAsync(jsonResponse);
    }
}