using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Drivers;
using Models.Types;
using Models.Users;
using System.Runtime.InteropServices.Marshalling;
using Models.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Models.LicenseModels;

namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
{

    public GovConnectDbContext(DbContextOptions<GovConnectDbContext> options) : base(options)
    {

    }

    public GovConnectDbContext()
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationFees>()
            .HasKey(appFees => new { appFees.ApplicationTypeId, appFees.ApplicationForId });

        modelBuilder.Entity<LicenseApplication>().HasOne(app => app.ApplicationFees)
            .WithMany(fees => fees.Applications)
            .HasForeignKey(appFees => new { appFees.ApplicationTypeId, appFees.ApplicationForId });

        modelBuilder.Entity<LicenseType>()
            .HasKey(license => license.Id);

        modelBuilder.Entity<LicenseType>()
            .HasData(
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


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Data Source=MOSTAFA-ALAA\\MMMSERVER;database=GovConnectDB;Integrated Security=True;Trust Server Certificate=True");

    }
}