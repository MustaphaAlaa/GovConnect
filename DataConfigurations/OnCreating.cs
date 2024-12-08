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

public partial class DVLDDbContext : IdentityDbContext<User, UserRoles, Guid>
{

    public DVLDDbContext(DbContextOptions<DVLDDbContext> options) : base(options)
    {

    }

    public DVLDDbContext()
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationFor>()
            .HasKey(app => app.Id);

        modelBuilder.Entity<ApplicationFor>()
            .Property(app => app.Id)
            .HasColumnType("smallint")
            .ValueGeneratedOnAdd();

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

        optionsBuilder.UseSqlServer("Data Source=MOSTAFA-ALAA\\MMMSERVER;database=LicenseHubDB;Integrated Security=True;Trust Server Certificate=True");

    }
}