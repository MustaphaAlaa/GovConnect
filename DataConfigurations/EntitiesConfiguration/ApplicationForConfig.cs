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
                Id = (short)EnApplicationFor.LocalLicense,
                For = EnApplicationFor.LocalLicense.ToString()
            },
            new ApplicationFor()
            {
                Id = (short)EnApplicationFor.InternationalLicense,
                For = EnApplicationFor.InternationalLicense.ToString()
            },
            new ApplicationFor()
            {
                Id = (short)EnApplicationFor.Passport,
                For = EnApplicationFor.Passport.ToString()
            },
            new ApplicationFor()
            {
                Id = (short)EnApplicationFor.NationalNumberId,
                For = EnApplicationFor.InternationalLicense.ToString()
            } 
        });
    }
}