using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public class ExpiredException(string message, Exception? innerException = null) : ApiExceptionBase(message, innerException);