using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using IServices.IValidators;

namespace Services.ApplicationServices.Validators;

public class CreateDateValidator : IDateValidator
{
    public void Validate(DateTime day)
    {
        try
        {
            if ((day < DateTime.Now) || (day > DateTime.Now.AddDays(14)))
                throw new ArgumentOutOfRangeException("Invalid Date Range");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}