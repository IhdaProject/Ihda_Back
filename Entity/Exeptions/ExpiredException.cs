namespace Entity.Exeptions.Eimzo;

public class ExpiredException : Exception
{
    public ExpiredException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}