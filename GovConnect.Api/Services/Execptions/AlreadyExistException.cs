namespace Services.Exceptions;

public class AlreadyExistException : System.Exception
{
    public AlreadyExistException()
    {
    }

    public AlreadyExistException(string? message) : base(message)
    {
    }

    public AlreadyExistException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}