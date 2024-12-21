using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ServicePuproseConfig : IEntityTypeConfiguration<ServicePurpose>
{
    public void Configure(EntityTypeBuilder<ServicePurpose> builder)
    {
        builder.HasKey(x => x.ServicePurposeId);

        builder.ToTable("ServicesPurposes");

        builder.HasData(new ServicePurpose[]
        {
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.New,
                Purpose = EnServicePurpose.New.ToString(),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.Renew,
                Purpose = EnServicePurpose.Renew.ToString(),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.Release,
                Purpose = EnServicePurpose.Release.ToString(),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.ReplacementForDamage,
                Purpose = EnServicePurpose.ReplacementForDamage.ToString(),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.ReplacementForLost,
                Purpose = EnServicePurpose.ReplacementForLost.ToString(),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.RetakeTest,
                Purpose = EnServicePurpose.RetakeTest.ToString(),
            },
        });
    }
}