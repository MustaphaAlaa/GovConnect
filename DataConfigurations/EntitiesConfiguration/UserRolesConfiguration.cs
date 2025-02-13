using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataConfigurations.EntitiesConfiguration;

public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.NormalizedName).IsUnique();

        builder.HasData(new UserRoles[]{
            new UserRoles{
                Id = Guid.NewGuid(),
                Name = EnUserRoles.User.ToString(),
                NormalizedName =  EnUserRoles.User.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new UserRoles{
                Id = Guid.NewGuid(),
                Name = EnUserRoles.Admin.ToString(),
                NormalizedName = EnUserRoles.Admin.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new UserRoles{
                Id = Guid.NewGuid(),
                Name = EnUserRoles.Employee.ToString(),
                NormalizedName = EnUserRoles.Employee.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
        });
    }
}