using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Users;

namespace DataConfigurations.EntitiesConfiguration;

/// <summary>
/// Conifguration for Employees Table
/// </summary>
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {

        ///<summary>
        /// Dummy Employee data for testing purposes
        /// </summary>
        var emps = new Employee[]
        {
            new Employee
            {
                //Dummy EMployees
                
                Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                 HiredByAdmin  = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                HiredDate = new DateTime(2025,5,1),
                IsActive = true,
                UserId  = new Guid("11111111-1111-1111-1111-111111111111")
            },
           new Employee
            {
                //Dummy EMployees
               Id = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"),
               HiredByAdmin = new Guid("123e4567-e89b-12d3-a456-426614174000"),
                HiredDate = new DateTime(2026, 6, 1),
                IsActive = true,
                UserId  = new Guid("22222222-2222-2222-2222-222222222222")
            },
           new Employee
            {
                //Dummy EMployees
                Id = new Guid("1b9d6bcd-bbfd-4b2d-9b5d-ab8dfbbd4bed"),
                 HiredByAdmin = new Guid("123e4567-e89b-12d3-a456-426614174000"),
                 HiredDate = new DateTime(2023, 6, 1),
                    IsActive = true,
                    UserId  = new Guid("22222222-2222-2222-2222-222222222222")
            }
        };

        builder.HasData(emps);
    }
}
