using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Countries;

namespace DataConfigurations.EntitiesConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries"); // Optional: explicitly set the table CountryName
            builder.HasKey(c => c.CountryId);
            builder.Property(c => c.CountryName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.CountryCode).IsRequired().HasMaxLength(5);


            builder.HasData(CreateCountries());
        }

        private static Country[] CreateCountries()
        {
            var countries = new List<Country>();
            foreach (var countryCode in Enum.GetValues(typeof(EnCountryCode)))
            {
                countries.Add(new Country
                {
                    CountryId = (int)countryCode,
                    CountryName = ((EnCountryName)countryCode).ToString()
                        .Replace("_", " "), // To display space instead of underscore
                    CountryCode = ((EnCountryCode)countryCode).ToString()
                });
            }

            return countries.ToArray();
        }
    }
}