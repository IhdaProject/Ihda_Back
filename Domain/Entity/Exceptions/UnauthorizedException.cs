using Entity.Enums;
using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public class UnauthorizedException : ApiExceptionBase
{
    public override int StatusCode => 401;

    public UnauthorizedException(string message) : base(message)
    {
    }
    public UnauthorizedException(string message, ErrorTypes errorCode = ErrorTypes.ServerIntervalError) : base(message)
    {
        ErrorCode = errorCode;
    }
}