using Entity.Exeptions.Common;

namespace Entity.Exeptions;

public sealed class AlreadyExistsException : ApiExceptionBase
{
    public AlreadyExistsException(string message,int errorCode = 403) : base(message)
    {
        StatusCode = 403;
        ErrorCode = errorCode;
    }
}