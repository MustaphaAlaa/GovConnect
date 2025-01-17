using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Tests;
using Models.Tests.Enums;

namespace DataConfigurations.EntitiesConfiguration;

public class TimeIntervalConfiguration : IEntityTypeConfiguration<TimeInterval>
{
    public void Configure(EntityTypeBuilder<TimeInterval> builder)
    {
        builder.HasKey(timeInterval => timeInterval.TimeIntervalId);
        builder.HasData(CreateTimeIntervals());
    }

    private static TimeInterval[] CreateTimeIntervals()
    {
        var timeIntervals = new List<TimeInterval>();
        int id = 1;
        foreach (var hour in Enum.GetValues(typeof(EnHour)))
        {
            foreach (var stage in Enum.GetValues(typeof(EnHourStage)))
            {
                timeIntervals.Add(new TimeInterval { TimeIntervalId = id++, Hour = (EnHour)hour, Minute = (EnHourStage)stage });
            }
        }
        return timeIntervals.ToArray();
    }
}

public class LDLApplicationsAllowedToRetakeATestConfiguration : IEntityTypeConfiguration<LDLApplicationsAllowedToRetakeATest>
{
    public void Configure(EntityTypeBuilder<LDLApplicationsAllowedToRetakeATest> builder)
    {
        builder.HasKey(re => re.Id);

        builder.Property(re => re.LocalDrivingLicenseApplicationId)
            .HasColumnName("LocalDrivingLicenseApplicationId");
    }


}