using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ApplicationTypeConfig : IEntityTypeConfiguration<ApplicationType>
{
    public void Configure(EntityTypeBuilder<ApplicationType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("ApplicationTypes");

        builder.HasData(new ApplicationType[]
        {
            new ApplicationType()
            {
                Id = (byte)enApplicationType.New,
                Type = enApplicationType.New.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)enApplicationType.Renew,
                Type = enApplicationType.Renew.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)enApplicationType.Release,
                Type = enApplicationType.Release.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)enApplicationType.ReplacementForDamge,
                Type = enApplicationType.ReplacementForDamge.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)enApplicationType.ReplacementForLost,
                Type = enApplicationType.ReplacementForLost.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)enApplicationType.RetakeTest,
                Type = enApplicationType.RetakeTest.ToString(),
            },
        });
    }
}