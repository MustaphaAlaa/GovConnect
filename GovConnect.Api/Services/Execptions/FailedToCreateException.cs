namespace Services.Exceptions
{
    public class FailedToCreateException : System.Exception
    {
        public FailedToCreateException()
        {
        }

        public FailedToCreateException(string? message) : base(message)
        {
        }

        public FailedToCreateException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}