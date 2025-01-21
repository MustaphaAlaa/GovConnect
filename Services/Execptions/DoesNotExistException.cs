namespace Services.Exceptions
{
    public class DoesNotExistException : System.Exception
    {
        public DoesNotExistException()
        {
        }

        public DoesNotExistException(string? message) : base(message)
        {
        }

        public DoesNotExistException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}