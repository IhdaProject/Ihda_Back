using Entity.Enums;
using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class NotFoundException : ApiExceptionBase
{
    public NotFoundException(string message, ErrorTypes errorCode = ErrorTypes.ServerIntervalError) : base(message)
    {
        StatusCode = 404;
        ErrorCode = errorCode;
    }

    public static void ThrowIfNull(object? data, string message = "Not found", ErrorTypes errorCode = ErrorTypes.ServerIntervalError)
    {
        if (data is null) throw new NotFoundException(message, errorCode);
    }
}