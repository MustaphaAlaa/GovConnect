using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ApplicationForConfig : IEntityTypeConfiguration<ApplicationFor>
{
    public void Configure(EntityTypeBuilder<ApplicationFor> builder)
    {
        builder.HasKey(app => app.Id);

        builder.Property(app => app.Id)
            .HasColumnType("smallint")
            .ValueGeneratedOnAdd();

        builder.HasData(new ApplicationFor[]
        {
            new ApplicationFor()
            {
                Id = (short)enApplicationFor.LocalLicense,
                For = enApplicationFor.LocalLicense.ToString()
            },
            new ApplicationFor()
            {
                Id = (short)enApplicationFor.InternationalLicense,
                For = enApplicationFor.InternationalLicense.ToString()
            },
            new ApplicationFor()
            {
                Id = (short)enApplicationFor.Passport,
                For = enApplicationFor.Passport.ToString()
            },
            new ApplicationFor()
            {
                Id = (short)enApplicationFor.NationalNumberId,
                For = enApplicationFor.InternationalLicense.ToString()
            } 
        });
    }
}