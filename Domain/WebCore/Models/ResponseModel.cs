using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebCore.Models;

public class ResponseModel
{
    [JsonInclude] public Guid Id = Guid.NewGuid();
    public HttpStatusCode Code { get; init; } = HttpStatusCode.OK;
    public object? Content { get; init; }
    public string? Error { get; init; }
    public int? Total { get; set; }
    public List<ModelErrorState>? ModelStateError { get; init; }
    [JsonIgnore] public string? StackTrace { get; init; }

    public ResponseModel(Exception? exception = null, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        this.Error = exception?.Message;
        this.Code = code;
        this.StackTrace = exception?.StackTrace;
    }

    public ResponseModel(object content, HttpStatusCode code = HttpStatusCode.OK)
    {
        this.Content = content;
        this.Code = code;
    }

    public ResponseModel(IEnumerable<object> content, int total, HttpStatusCode code = HttpStatusCode.OK)
    {
        this.Content = content;
        this.Code = code;
        this.Total = total;
    }

    public ResponseModel(HttpStatusCode code)
    {
        this.Code = code;
    }

    public ResponseModel(ModelStateDictionary modelState, Exception? exception = null)
    {
        this.Code = HttpStatusCode.BadRequest;
        this.ModelStateError = modelState
            .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
            .Select(x => new ModelErrorState()
            {
                Key = x.Key,
                ErrorMessage = x.Value.Errors.FirstOrDefault().ErrorMessage
            }).ToList();

        var errorResponse = new ResponseModel(exception);
        this.Error = errorResponse.Error;
        this.StackTrace = errorResponse.StackTrace;
    }

    public ResponseModel(string message, HttpStatusCode code = HttpStatusCode.OK)
    {
        this.Content = message;
        this.Code = code;
    }


    public static ResponseModel ResultFromException(Exception exception,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) =>
        new ResponseModel(exception, statusCode);

    public static ResponseModel ResultFromModelState(ModelStateDictionary modelState, Exception? exception = null) =>
        new ResponseModel(modelState, exception);

    public static ResponseModel ResultFromContent(object content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
        new ResponseModel(content, statusCode);

    public static implicit operator ResponseModel(string s) => new ResponseModel(s, HttpStatusCode.OK);

    public static ResponseModel FromIConvertible(IConvertible s) => new ResponseModel(s, HttpStatusCode.OK);

    public static implicit operator ResponseModel((string content, int statusCode) data) =>
        new ResponseModel(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel((IConvertible content, int statusCode) data) =>
        new ResponseModel(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel(Exception exception) =>
        new ResponseModel(exception, HttpStatusCode.InternalServerError);

    public static implicit operator ResponseModel((Exception exception, int statusCode) data) =>
        new ResponseModel(data.exception, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel((IEnumerable<object> items, int total) data) =>
        new ResponseModel(data.items, data.total);

    public static implicit operator ResponseModel((IEnumerable<IComparable> items, int total) data) =>
        new ResponseModel(data.items, data.total);

    public static implicit operator ResponseModel((IEnumerable<object> items, int total, int statusCode) data) =>
        new ResponseModel(data.items, data.total, (HttpStatusCode)data.statusCode);

    public static implicit operator
        ResponseModel((IEnumerable<IComparable> items, int total, int statusCode) data) =>
        new ResponseModel(data.items, data.total, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel((object content, HttpStatusCode statusCode) data) =>
        new ResponseModel(data.content, (HttpStatusCode)data.statusCode);
    
    public static implicit operator ResponseModel((object content, int statusCode) data) =>
        new ResponseModel(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel(int code = (int)HttpStatusCode.OK) =>
        new ResponseModel("Empty Response", (HttpStatusCode)code);
}

public class ModelErrorState
{
    public string Key { get; set; }
    public string ErrorMessage { get; set; }
}

public class ResponseModel<T>
{
    [JsonInclude] public Guid Id = Guid.NewGuid();
    public HttpStatusCode Code { get; init; } = HttpStatusCode.OK;
    public T? Content { get; init; }
    public string? Error { get; init; }
    public int? Total { get; set; }
    public List<ModelErrorState>? ModelStateError { get; init; }
    [JsonIgnore] public string? StackTrace { get; init; }

    public ResponseModel(Exception? exception = null, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        this.Error = exception?.Message;
        this.Code = code;
        this.StackTrace = exception?.StackTrace;
    }

    public ResponseModel(T content, HttpStatusCode code = HttpStatusCode.OK)
    {
        this.Content = content;
        this.Code = code;
    }

    public ResponseModel(HttpStatusCode code)
    {
        this.Code = code;
    }

    public ResponseModel(ModelStateDictionary modelState, Exception? exception = null)
    {
        this.Code = HttpStatusCode.BadRequest;
        this.ModelStateError = modelState
            .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
            .Select(x => new ModelErrorState()
            {
                Key = x.Key,
                ErrorMessage = x.Value.Errors.FirstOrDefault().ErrorMessage
            }).ToList();

        var errorResponse = new ResponseModel(exception);
        this.Error = errorResponse.Error;
        this.StackTrace = errorResponse.StackTrace;
    }

    public static ResponseModel ResultFromException(Exception exception,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) =>
        new ResponseModel(exception, statusCode);

    public static ResponseModel ResultFromModelState(ModelStateDictionary modelState, Exception? exception = null) =>
        new ResponseModel(modelState, exception);

    public static ResponseModel<T> ResultFromContent(T content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
        new ResponseModel<T>(content, statusCode);

    public static implicit operator ResponseModel<T>(Exception exception) =>
        new ResponseModel<T>(exception, HttpStatusCode.InternalServerError);

    public static implicit operator ResponseModel<T>(T data) =>
        new ResponseModel<T>(data, HttpStatusCode.OK);

    public static implicit operator ResponseModel<T>((Exception exception, int statusCode) data) =>
        new ResponseModel<T>(data.exception, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel<T>((T content, int statusCode) data) =>
        new ResponseModel<T>(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel<T>(int code = (int)HttpStatusCode.OK) =>
        new ResponseModel<T>((HttpStatusCode)code);
}