using System;
using RepositoryContracts;

namespace WebAPI;

public class GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger) : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException nEx)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(nEx.Message);
        }
        catch (Exception ex)
        {

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(ex.Message);

        }
    }
}
