using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Drivers;
using Models.Types;
using Models.Users;
using System.Runtime.InteropServices.Marshalling;
using Models.Applications;
using Microsoft.Extensions.Configuration;

namespace DataConfigurations;

public partial class DVLDDbContext : IdentityDbContext<User, UserRoles, Guid>
{
 
    public DVLDDbContext(DbContextOptions<DVLDDbContext> options ) : base(options)
    {
        
    }

    public DVLDDbContext()
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationFees>()
            .HasKey(appFees => new { appFees.ApplicationTypeId, appFees.ApplicationForId });

        modelBuilder.Entity<Application>().HasOne(app => app.ApplicationFees)
            .WithMany(fees => fees.Applications)
            .HasForeignKey(appFees => new { appFees.ApplicationTypeId, appFees.ApplicationForId });
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Data Source=MOSTAFA-ALAA\\MMMSERVER;database=DVLD;Integrated Security=True;Trust Server Certificate=True" );

    }
}