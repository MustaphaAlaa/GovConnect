namespace Services.Execptions
{
    public class FailedToCreateException : Exception
    {
        public FailedToCreateException()
        {
        }

        public FailedToCreateException(string? message) : base(message)
        {
        }

        public FailedToCreateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}