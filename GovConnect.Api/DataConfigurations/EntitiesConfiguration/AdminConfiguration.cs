using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Users;

namespace DataConfigurations.EntitiesConfiguration;

/// <summary>
/// Conifguration for Employees Table
/// </summary>
public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {

        ///<summary>
        /// Dummy Employee data for testing purposes
        /// </summary>
        var emps = new Admin[]
        {
            new Admin
            {
                //Dummy EMployees
                
              Id = new Guid("123e4567-e89b-12d3-a456-426614174000"),
              IsEmployee = true,
                UserId  = new Guid("11111111-1111-1111-1111-111111111111")
            },
           new Admin
            {
                //Dummy EMployees
                 Id = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                  IsEmployee = false,
                UserId  = new Guid("22222222-2222-2222-2222-222222222222")

            }
        };

        builder.HasData(emps);
    }
}
