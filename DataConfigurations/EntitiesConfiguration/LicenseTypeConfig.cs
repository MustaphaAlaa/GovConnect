using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.LicenseModels;

namespace DataConfigurations.EntitiesConfiguration;

public class LicenseTypeConfig: IEntityTypeConfiguration<LicenseType>   
{
    public void Configure(EntityTypeBuilder<LicenseType> builder)
    {
        
        builder.HasKey(license => license.Id);

        builder.HasData(
                new LicenseType()
                {
                    Id = (byte)enLicenseType.International,
                    Title = enLicenseType.International.ToString(),
                    Fees = 100
                },
                new LicenseType()
                {
                    Id = (byte)enLicenseType.Local,
                    Title = enLicenseType.Local.ToString(),
                    Fees = 20
                }
            );
    }
}