using ModelDTO.TestsDTO;
using Models.Tests;

namespace IRepository.ITestRepos;

public interface ITestTypePassedChecker
{
    Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId);
}


public interface ITestResultInfoRetrieve{
    IQueryable<TestResultInfo>  GetTestResultInfo(int LDLApplicationId, int TestTypeId);
}