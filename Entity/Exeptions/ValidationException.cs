using Entity.Exeptions.Common;

namespace Entity.Exeptions;

internal class ValidationException : ApiExceptionBase
{
    public ValidationException(string message) : base(message)
    {
        this.StatusCode = 400;
    }
}