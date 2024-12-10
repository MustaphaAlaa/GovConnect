using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ApplicationFeesConfig : IEntityTypeConfiguration<ApplicationFees>
{
    public void Configure(EntityTypeBuilder<ApplicationFees> builder)
    {
        builder.HasKey(appFees => new { appFees.ApplicationTypeId, appFees.ApplicationForId });
    }
}