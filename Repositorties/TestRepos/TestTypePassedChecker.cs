using DataConfigurations;
using IServices.ITests.ITest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorties.TestRepos;

public class TestTypePassedChecker : ITestTypePassedChecker
{

    private readonly GovConnectDbContext _govConnectDbContext;
    private readonly ILogger<TestTypePassedChecker> _logger;
    public TestTypePassedChecker(GovConnectDbContext govConnectDbContext, ILogger<TestTypePassedChecker> logger)
    {
        _govConnectDbContext = govConnectDbContext;
        _logger = logger;
    }

    public async Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId)
    {
        _logger.LogInformation($"{this.GetType().Name} -- IsTestTypePassed");

        var res = await _govConnectDbContext.Tests
            .Join(_govConnectDbContext.Bookings,
                    test => test.BookingId,
                    booking => booking.BookingId,
                    (test, booking) => new { test, booking })
            .AnyAsync(x => x.booking.LocalDrivingLicenseApplicationId == LDLApplicationId &&
                            x.booking.TestTypeId == TestTypeId
                            && x.test.TestResult == true);

        return res;
    }
}
