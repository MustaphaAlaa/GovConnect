namespace Services.Exceptions
{
    public class FailedToUpdateException : System.Exception
    {
        public FailedToUpdateException()
        {
        }

        public FailedToUpdateException(string? message) : base(message)
        {
        }

        public FailedToUpdateException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}