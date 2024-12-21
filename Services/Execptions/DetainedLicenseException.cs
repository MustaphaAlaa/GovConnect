namespace Services.Execptions;

public class DetainedLicenseException : Exception
{
    public DetainedLicenseException()
    {
    }

    public DetainedLicenseException(string? message) : base(message)
    {
    }

    public DetainedLicenseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}