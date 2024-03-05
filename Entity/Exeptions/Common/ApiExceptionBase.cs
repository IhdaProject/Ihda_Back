namespace Entity.Exeptions.Common;

public class ApiExceptionBase : Exception
{
    public virtual int StatusCode { get; set; }

    public ApiExceptionBase(string message) : base(message)
    {
    }

    public ApiExceptionBase(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public ApiExceptionBase(Exception exception) : base(exception.Message, exception)
    {
        this.StatusCode = 500;
    }
}