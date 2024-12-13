using Entity.Exeptions.Common;

namespace Entity.Exeptions;

public sealed class NotFoundException : ApiExceptionBase
{
    public NotFoundException(string message, int errorCode = 404) : base(message)
    {
        StatusCode = 404;
        ErrorCode = errorCode;
    }

    public static void ThrowIfNull(object? data, string message = "Not found", int errorCode = 404)
    {
        if (data is null) throw new NotFoundException(message, errorCode);
    }
}