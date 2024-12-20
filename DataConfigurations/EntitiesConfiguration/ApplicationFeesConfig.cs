using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ApplicationFeesConfig : IEntityTypeConfiguration<ServiceFees>
{
    public void Configure(EntityTypeBuilder<ServiceFees> builder)
    {
        builder.HasKey(appFees => new { appFees.ServicePurposeId, ApplicationForId = appFees.ServiceCategoryId });
    }
}