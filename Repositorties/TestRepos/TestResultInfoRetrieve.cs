using DataConfigurations;
using IRepository.ITestRepos;
using Microsoft.Extensions.Logging;
using ModelDTO.TestsDTO;

namespace Repositorties.TestRepos;

public class TestResultInfoRetrieve : ITestResultInfoRetrieve
{

  private readonly GovConnectDbContext _govConnectDbContext;
    private readonly ILogger<TestResultInfoRetrieve> _logger;
    public TestResultInfoRetrieve(GovConnectDbContext govConnectDbContext, ILogger<TestResultInfoRetrieve> logger)
    {
        _govConnectDbContext = govConnectDbContext;
        _logger = logger;
    }


    public   IQueryable<TestResultInfo>   GetTestResultInfo(int LDLApplicationId, int TestTypeId)
    {
                _logger.LogInformation($"{this.GetType().Name} -- GetTestResultInfo");

        var res =   _govConnectDbContext.Tests
            .Join(_govConnectDbContext.Bookings,
                    test => test.BookingId,
                    booking => booking.BookingId,
                    (test, booking) => new TestResultInfo { Test = test, Booking =  booking });
        return res;
    }
}