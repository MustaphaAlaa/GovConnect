using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Drivers;
using Models.Types;
using Models.Users;

namespace DataConfigurations;

public partial class DVLDDbContext
{
    private readonly Guid userID;

    public DVLDDbContext(DbContextOptions options) : base(options)
    {
        /*userID = Guid.NewGuid();*/
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



    }
}