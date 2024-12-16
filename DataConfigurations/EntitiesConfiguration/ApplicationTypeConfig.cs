using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ApplicationTypeConfig : IEntityTypeConfiguration<ApplicationPurpose>
{
    public void Configure(EntityTypeBuilder<ApplicationPurpose> builder)
    {
        builder.HasKey(x => x.ApplicationPurposeId);

        builder.ToTable("ApplicationTypes");

        builder.HasData(new ApplicationPurpose[]
        {
            new ApplicationPurpose()
            {
                ApplicationPurposeId = (byte)EnServicePurpose.New,
                Purpose = EnServicePurpose.New.ToString(),
            },
            new ApplicationPurpose()
            {
                ApplicationPurposeId = (byte)EnServicePurpose.Renew,
                Purpose = EnServicePurpose.Renew.ToString(),
            },
            new ApplicationPurpose()
            {
                ApplicationPurposeId = (byte)EnServicePurpose.Release,
                Purpose = EnServicePurpose.Release.ToString(),
            },
            new ApplicationPurpose()
            {
                ApplicationPurposeId = (byte)EnServicePurpose.ReplacementForDamge,
                Purpose = EnServicePurpose.ReplacementForDamge.ToString(),
            },
            new ApplicationPurpose()
            {
                ApplicationPurposeId = (byte)EnServicePurpose.ReplacementForLost,
                Purpose = EnServicePurpose.ReplacementForLost.ToString(),
            },
            new ApplicationPurpose()
            {
                ApplicationPurposeId = (byte)EnServicePurpose.RetakeTest,
                Purpose = EnServicePurpose.RetakeTest.ToString(),
            },
        });
    }
}