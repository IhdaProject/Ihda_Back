using Entity.Enums;
using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class AlreadyExistsException : ApiExceptionBase
{
    public AlreadyExistsException(string message,ErrorTypes errorCode = ErrorTypes.ServerIntervalError) : base(message)
    {
        StatusCode = 403;
        ErrorCode = errorCode;
    }
}