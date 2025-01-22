namespace IRepository.ITestRepos;

public interface ITestTypePassedChecker
{
    Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId);
}