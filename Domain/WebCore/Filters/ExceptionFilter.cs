using System.Net;
using Entity.Exceptions.Common;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace WebCore.Filters;

public class ExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        context.ExceptionHandled = true;
        context.HttpContext.Response.StatusCode =
            exception is ApiExceptionBase apiException
                ? apiException.StatusCode
                : (int)HttpStatusCode.InternalServerError;
        context.Result =
            new JsonResult(ResponseModel.ResultFromException(exception,
                (HttpStatusCode)context.HttpContext.Response.StatusCode));

        Log.Error(exception, "Exception: " + exception.Message);
        return Task.CompletedTask;
    }
}