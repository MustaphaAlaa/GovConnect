using Microsoft.EntityFrameworkCore;


namespace DataConfigurations;

public static class DbFunctionConfigurations
{

    public static void ConfigureFunctions(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDbFunction(() => new GovConnectDbContext(null!).GetAvailableDays(default))
           .HasName("GetAvailableDays");

        modelBuilder.HasDbFunction(() => new GovConnectDbContext(null!).GetLDLAppsAllowedToRetakATest(default, default))
            .HasName("GetLDLAppsAllowedToRetakATest");

        modelBuilder.HasDbFunction(() => new GovConnectDbContext(null!).GetPassedTest(default, default))
           .HasName("GetPassedTest");

        modelBuilder.HasDbFunction(() => new GovConnectDbContext(null!).GetTestResult(default))
           .HasName("GetTestResult");

        modelBuilder.HasDbFunction(() => new GovConnectDbContext(null!).GetTestResultForABookingId(default))
           .HasName("GetTestResultForABookingId");


        modelBuilder.HasDbFunction(() => new GovConnectDbContext(null!).GetTestTypeDayTimeInterval(default, default))
           .HasName("GetTestTypeDayTimeInterval");


    }
}


