using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Users;
using Microsoft.Extensions.Configuration;
using ModelDTO.TestsDTO;


namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
{

    //IConfiguration _configuration;

    //public GovConnectDbContext(DbContextOptions<GovConnectDbContext> options, IConfiguration configuration) : base(options)
    //{
    //    _configuration = configuration;

    //}

    public GovConnectDbContext(DbContextOptions<GovConnectDbContext> options) : base(options)
    {


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovConnectDbContext).Assembly);

        modelBuilder.Entity<AvailableDay>().HasNoKey().ToView(null);
        modelBuilder.Entity<TestTypeResult>().HasNoKey().ToView(null);


        if (!this.IsMigration())
        {
            DbFunctionConfigurations.ConfigureFunctions(modelBuilder);
        }

    }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.UseSqlServer(_configuration.GetConnectionString("default"));
    //}
}
