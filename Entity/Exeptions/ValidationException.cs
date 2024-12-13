using Entity.Exeptions.Common;

namespace Entity.Exeptions;

internal sealed class ValidationException : ApiExceptionBase
{
    public ValidationException(string message, int errorCode = 400) : base(message)
    {
        StatusCode = 400;
        ErrorCode = errorCode;
    }
}