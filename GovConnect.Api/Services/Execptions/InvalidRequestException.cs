namespace Services.Exceptions;
public class InvalidRequestException : System.Exception
{
    public InvalidRequestException()
    {
    }

    public InvalidRequestException(string? message) : base(message)
    {
    }

    public InvalidRequestException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
