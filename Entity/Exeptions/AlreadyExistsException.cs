using Entity.Exeptions.Common;

namespace Entity.Exeptions;

public class AlreadyExistsException : ApiExceptionBase
{
    public AlreadyExistsException(string message) : base(message)
    {
        this.StatusCode = 403;
    }
}