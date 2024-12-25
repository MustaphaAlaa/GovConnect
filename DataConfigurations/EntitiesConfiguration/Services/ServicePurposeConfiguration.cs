using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ServicePurposeConfiguration : IEntityTypeConfiguration<ServicePurpose>
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
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Damage,
                Purpose = EnServicePurpose.Replacement_For_Damage.ToString().Replace("_"," "),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Lost,
                Purpose = EnServicePurpose.Replacement_For_Lost.ToString().Replace("_"," "),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.Release,
                Purpose = EnServicePurpose.Release.ToString(),
            },
            new ServicePurpose()
            {
                ServicePurposeId = (byte)EnServicePurpose.Retake_Test,
                Purpose = EnServicePurpose.Retake_Test.ToString().Replace("_"," "),
            },
        });
    }
}