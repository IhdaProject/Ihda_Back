namespace Entity.Exeptions.Common;

public class ApiExceptionBase : Exception
{
    public virtual int StatusCode { get; set; }
    public virtual int ErrorCode { get; set; }

    public ApiExceptionBase(string message) : base(message)
    {
    }

    public ApiExceptionBase(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public ApiExceptionBase(Exception exception) : base(exception.Message, exception)
    {
        StatusCode = 500;
        ErrorCode = 500;
    }
}