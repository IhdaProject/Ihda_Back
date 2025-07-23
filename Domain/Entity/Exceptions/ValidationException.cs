using Entity.Enums;
using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class ValidationException : ApiExceptionBase
{
    public ValidationException(string message, ErrorTypes errorCode = ErrorTypes.ServerIntervalError) : base(message)
    {
        StatusCode = 400;
        ErrorCode = errorCode;
    }
}