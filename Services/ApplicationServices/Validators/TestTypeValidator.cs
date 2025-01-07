using IServices.IValidators;
using Models.Tests;
using Services.Execptions;

namespace Services.ApplicationServices.Validators;

public class TestTypeValidator : ITestTypeValidator
{
    public void Validate(int testTypeId)
    {
        if (Enum.IsDefined(typeof(TestType), testTypeId))
            throw new DoesNotExistException();
    }
}