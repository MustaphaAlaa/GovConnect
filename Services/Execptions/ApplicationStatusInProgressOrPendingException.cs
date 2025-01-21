namespace Services.Exceptions;

public class ApplicationStatusInProgressOrPendingException : System.Exception
{
    public ApplicationStatusInProgressOrPendingException()
    {
    }

    public ApplicationStatusInProgressOrPendingException(string? message) : base(message)
    {
    }

    public ApplicationStatusInProgressOrPendingException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}