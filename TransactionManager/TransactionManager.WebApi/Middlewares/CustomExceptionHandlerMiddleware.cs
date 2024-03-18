using System.Net;
using TransactionManager.Application.Exceptions;

namespace TransactionManager.WebApi.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;

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

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        if (exception is BusinessLogicException)
        {
            code = HttpStatusCode.BadRequest;
            result = exception.Message;
        }

        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = exception.Message;
        }

        return context.Response.WriteAsync(result);
    }
}