using DataConfigurations;
using IRepository.ITestRepos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repositorties.TestRepos;

public class TestTypePassedChecker : ITestTypePassedChecker
{

    private readonly GovConnectDbContext _govConnectDbContext;
    private readonly ILogger<TestTypePassedChecker> _logger;
    private readonly  ITestResultInfoRetrieve _testResultInfoRetrieve;
    public TestTypePassedChecker(GovConnectDbContext govConnectDbContext,
     ILogger<TestTypePassedChecker> logger,
     ITestResultInfoRetrieve testResultInfoRetrieve)
    {
        _govConnectDbContext = govConnectDbContext;
        _testResultInfoRetrieve = testResultInfoRetrieve;
        _logger = logger;
    }

    public async Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId)
    {
        _logger.LogInformation($"{this.GetType().Name} -- IsTestTypePassed");

        var res = await _testResultInfoRetrieve.GetTestResultInfo(LDLApplicationId, TestTypeId)
            .AnyAsync(x => x.Booking.LocalDrivingLicenseApplicationId == LDLApplicationId &&
                            x.Booking.TestTypeId == TestTypeId
                            && x.Test.TestResult == true);

        return res;
    }
}
