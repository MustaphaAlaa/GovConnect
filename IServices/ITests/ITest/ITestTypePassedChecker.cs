namespace IServices.ITests.ITest;

public interface ITestTypePassedChecker
{
    Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId);
}