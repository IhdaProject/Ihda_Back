using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Entity.Models.ApiModels;

public class ResponseModel
{
    [JsonInclude] public Guid Id = Guid.NewGuid();
    public HttpStatusCode Code { get; init; }
    public object? Content { get; init; }
    public string? Error { get; init; }
    public int? Total { get; set; }
    public List<ModelErrorState>? ModelStateError { get; init; }
    [JsonIgnore] public string? StackTrace { get; init; }

    public ResponseModel(Exception? exception = null, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        Error = exception?.Message;
        Code = code;
        StackTrace = exception?.StackTrace;
    }

    public ResponseModel(object content, HttpStatusCode code = HttpStatusCode.OK)
    {
        Content = content;
        Code = code;
    }

    public ResponseModel(IEnumerable<object> content, int total, HttpStatusCode code = HttpStatusCode.OK)
    {
        Content = content;
        Code = code;
        Total = total;
    }

    public ResponseModel(HttpStatusCode code)
    {
        Code = code;
    }

    public ResponseModel(ModelStateDictionary modelState, Exception? exception = null)
    {
        Code = HttpStatusCode.BadRequest;
        ModelStateError = modelState
            .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
            .Select(x => new ModelErrorState()
            {
                Key = x.Key,
                ErrorMessage = x.Value.Errors.FirstOrDefault().ErrorMessage
            }).ToList();

        var errorResponse = new ResponseModel(exception);
        Error = errorResponse.Error;
        StackTrace = errorResponse.StackTrace;
    }

    public ResponseModel(string message, HttpStatusCode code = HttpStatusCode.OK)
    {
        Content = message;
        Code = code;
    }


    public static ResponseModel ResultFromException(Exception exception,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) =>
        new(exception, statusCode);

    public static ResponseModel ResultFromModelState(ModelStateDictionary modelState, Exception? exception = null) =>
        new(modelState, exception);

    public static ResponseModel ResultFromContent(object content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
        new(content, statusCode);

    public static implicit operator ResponseModel(string s) => new ResponseModel(s, HttpStatusCode.OK);

    public static implicit operator ResponseModel((string content, int statusCode) data) =>
        new(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel((IConvertible content, int statusCode) data) =>
        new(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel(Exception exception) =>
        new(exception);

    public static implicit operator ResponseModel((Exception exception, int statusCode) data) =>
        new(data.exception, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel((IEnumerable<object> items, int total) data) =>
        new(data.items, data.total);

    public static implicit operator ResponseModel((IEnumerable<IComparable> items, int total) data) =>
        new(data.items, data.total);

    public static implicit operator ResponseModel((IEnumerable<object> items, int total, int statusCode) data) =>
        new(data.items, data.total, (HttpStatusCode)data.statusCode);

    public static implicit operator
        ResponseModel((IEnumerable<IComparable> items, int total, int statusCode) data) =>
        new(data.items, data.total, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel((object content, HttpStatusCode statusCode) data) =>
        new(data.content, data.statusCode);
    
    public static implicit operator ResponseModel((object content, int statusCode) data) =>
        new ResponseModel(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel(int code) =>
        new("Empty Response", (HttpStatusCode)code);
}

public class ModelErrorState
{
    public string Key { get; set; }
    public string? ErrorMessage { get; set; }
}

public class ResponseModel<T>
{
    [JsonInclude] public Guid Id = Guid.NewGuid();
    public HttpStatusCode Code { get; init; }
    public T? Content { get; init; }
    public string? Error { get; init; }
    public int? Total { get; set; }
    public List<ModelErrorState>? ModelStateError { get; init; }
    [JsonIgnore] public string? StackTrace { get; init; }

    public ResponseModel(Exception? exception = null, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        Error = exception?.Message;
        Code = code;
        StackTrace = exception?.StackTrace;
    }

    public ResponseModel(T content, HttpStatusCode code = HttpStatusCode.OK, int? total = null)
    {
        Content = content;
        Code = code;
        Total = total;
    }

    public ResponseModel(HttpStatusCode code)
    {
        Code = code;
    }

    public ResponseModel(ModelStateDictionary modelState, Exception? exception = null)
    {
        Code = HttpStatusCode.BadRequest;
        ModelStateError = modelState
            .Where(x => x.Value is { ValidationState: ModelValidationState.Invalid })
            .Select(x => new ModelErrorState()
            {
                Key = x.Key,
                ErrorMessage = x.Value?.Errors.FirstOrDefault()?.ErrorMessage
            }).ToList();

        var errorResponse = new ResponseModel(exception);
        Error = errorResponse.Error;
        StackTrace = errorResponse.StackTrace;
    }

    public static ResponseModel<T> ResultFromContent(T content, HttpStatusCode statusCode = HttpStatusCode.OK, int? total = null) =>
        new(content, statusCode,  total);

    public static implicit operator ResponseModel<T>(Exception exception) =>
        new(exception);

    public static implicit operator ResponseModel<T>(T data) =>
        new(data);

    public static implicit operator ResponseModel<T>((Exception exception, int statusCode) data) =>
        new(data.exception, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel<T>((T content, int statusCode) data) =>
        new(data.content, (HttpStatusCode)data.statusCode);

    public static implicit operator ResponseModel<T>(int code) =>
        new((HttpStatusCode)code);
}