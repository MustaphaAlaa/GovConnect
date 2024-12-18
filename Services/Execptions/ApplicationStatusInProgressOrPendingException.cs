namespace Services.Execptions;

public class ApplicationStatusInProgressOrPendingException : Exception
{
    public ApplicationStatusInProgressOrPendingException()
    {
    }

    public ApplicationStatusInProgressOrPendingException(string? message) : base(message)
    {
    }

    public ApplicationStatusInProgressOrPendingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}