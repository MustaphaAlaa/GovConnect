using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class LicenseApplicationConfig : IEntityTypeConfiguration<LicenseApplication>
{
    public void Configure(EntityTypeBuilder<LicenseApplication> builder)
    {
        builder.HasOne(app => app.ApplicationFees)
            .WithMany(fees => fees.Applications)
            .HasForeignKey(appFees => new { appFees.ApplicationTypeId, appFees.ApplicationForId });

    }
}