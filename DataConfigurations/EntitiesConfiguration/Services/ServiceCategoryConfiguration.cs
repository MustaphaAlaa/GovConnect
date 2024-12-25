using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
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
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                Category = EnServiceCategory.Local_Driving_License.ToString().Replace("_", " ")
            },
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.International_Driving_License,
                Category = EnServiceCategory.International_Driving_License.ToString().Replace("_", " ")
            },
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.Passport,
                Category = EnServiceCategory.Passport.ToString()
            },
            new ServiceCategory()
            {
                ServiceCategoryId = (short)EnServiceCategory.National_Identity_Card,
                Category = EnServiceCategory.National_Identity_Card.ToString().Replace("_", " ")
            }
        });
    }
}