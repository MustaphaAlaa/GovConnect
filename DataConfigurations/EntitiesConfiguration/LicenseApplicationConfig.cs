using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class LicenseApplicationConfig : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.HasOne(app => app.ServiceFees)
            .WithMany(fees => fees.Applications)
            .HasForeignKey(appFees => new { ApplicationTypeId = appFees.ServicePurposeId, ApplicationForId = appFees.ServiceCategoryId });

    }
}