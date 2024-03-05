using Entity.Exeptions.Common;

namespace Entity.Exeptions;

public class UnauthorizedException : ApiExceptionBase
{
    public override int StatusCode
    {
        get => 401;
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public UnauthorizedException(Exception exception) : base(exception)
    {
    }
}