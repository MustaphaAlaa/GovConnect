using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ApplicationForConfig : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.HasKey(app => app.ServiceCategoryId);

        builder.Property(app => app.ServiceCategoryId)
            .HasColumnType("smallint")
            .ValueGeneratedOnAdd();

        builder.HasData(new ServiceCategory[]
        {
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.LocalLicense,
                Category = EnServiceCategory.LocalLicense.ToString()
            },
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.InternationalLicense,
                Category = EnServiceCategory.InternationalLicense.ToString()
            },
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.Passport,
                Category = EnServiceCategory.Passport.ToString()
            },
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.NationalNumberId,
                Category = EnServiceCategory.InternationalLicense.ToString()
            } 
        });
    }
}