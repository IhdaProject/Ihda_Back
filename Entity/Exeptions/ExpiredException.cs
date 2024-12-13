using Entity.Exeptions.Common;

namespace Entity.Exeptions;

public class ExpiredException(string message, Exception? innerException = null) : ApiExceptionBase(message, innerException);