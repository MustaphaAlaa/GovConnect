using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;
using Models.Users;
using ModelDTO.TestsDTO;
using Microsoft.EntityFrameworkCore;
using Models.Tests;


namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
{
    // Define the TVF method
    public IQueryable<AvailableDay> GetAvailableDays(
        int testId)
    {
        return FromExpression(() => GetAvailableDays(testId));
    }

    public IQueryable<LDLApplicationsAllowedToRetakeATestDTO> GetLDLAppsAllowedToRetakATest(
        int ldlAppId, int testTypeId)
    {
        return FromExpression(() => GetLDLAppsAllowedToRetakATest(ldlAppId, testTypeId));
    }

    public IQueryable<TestDTO> GetPassedTest(
    int ldlApplicationId, int testId)
    {
        return FromExpression(() => GetPassedTest(ldlApplicationId, testId));
    }

    public IQueryable<TestDTO> GetTestResult(int testId)
    {
        return FromExpression(() => GetTestResult(testId));
    }

    public IQueryable<TestDTO> GetTestResultForABookingId(
        int bookingId)
    {
        return FromExpression(() => GetTestResultForABookingId(bookingId));
    }

    public IQueryable<TestDTO> GetTestTypeDayTimeInterval(
       int testTypeId, DateOnly day)
    {
        return FromExpression(() => GetTestTypeDayTimeInterval(testTypeId, day));
    }

}