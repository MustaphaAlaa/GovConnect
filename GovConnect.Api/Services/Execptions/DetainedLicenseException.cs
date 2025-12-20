namespace Services.Exceptions;

public class DetainedLicenseException : System.Exception
{
    public DetainedLicenseException()
    {
    }

    public DetainedLicenseException(string? message) : base(message)
    {
    }

    public DetainedLicenseException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}