using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Tests;

namespace DataConfigurations.EntitiesConfiguration;

public class LDLApplicationsAllowedToRetakeATestConfiguration : IEntityTypeConfiguration<LDLApplicationsAllowedToRetakeATest>
{
    public void Configure(EntityTypeBuilder<LDLApplicationsAllowedToRetakeATest> builder)
    {
        builder.HasKey(re => re.Id);

        builder.Property(re => re.LocalDrivingLicenseApplicationId)
            .HasColumnName("LocalDrivingLicenseApplicationId");
    }


}