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
                Id = (byte)EnApplicationType.New,
                Type = EnApplicationType.New.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)EnApplicationType.Renew,
                Type = EnApplicationType.Renew.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)EnApplicationType.Release,
                Type = EnApplicationType.Release.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)EnApplicationType.ReplacementForDamge,
                Type = EnApplicationType.ReplacementForDamge.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)EnApplicationType.ReplacementForLost,
                Type = EnApplicationType.ReplacementForLost.ToString(),
            },
            new ApplicationType()
            {
                Id = (byte)EnApplicationType.RetakeTest,
                Type = EnApplicationType.RetakeTest.ToString(),
            },
        });
    }
}