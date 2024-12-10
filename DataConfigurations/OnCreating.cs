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
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovConnectDbContext).Assembly);
     
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Data Source=MOSTAFA-ALAA\\MMMSERVER;database=GovConnectDB;Integrated Security=True;Trust Server Certificate=True");

    }
}