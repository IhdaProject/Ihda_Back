using System.Net;
using Entity.Exceptions.Common;
using Serilog;
using WebCore.Models;

namespace WebCore.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            if (e is ApiExceptionBase apiException)
            {
                context.Response.StatusCode = apiException.StatusCode;
                await context.Response.WriteAsJsonAsync(
                    ResponseModel.ResultFromException(e, (HttpStatusCode)apiException.StatusCode));
                Log.Error(e, "Exception: " + e.Message);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    ResponseModel.ResultFromException(e));
                Log.Fatal(e,"Exception: ");
            }
        }
    }
}