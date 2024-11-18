using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Drivers;
using Models.Types;
using Models.Users;
using System.Runtime.InteropServices.Marshalling;
using Models.Applications;

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
        
        
        
        modelBuilder.Entity<ApplicationFees>()
            .HasKey(appFees=> new {appFees.ApplicationTypeId, appFees.ApplicationForId });

        modelBuilder.Entity<Application>().HasOne(app => app.ApplicationFees)
            .WithMany(fees => fees.Applications)
            .HasForeignKey(appFees =>  new {appFees.ApplicationTypeId, appFees.ApplicationForId});



        /*var UserGuidId = Guid.NewGuid();
        modelBuilder.Entity<Country>().HasData(new Country[]
        {
            new Country() {Id=1 ,CountryName = "Egypt"},
            new Country() {Id=2 ,CountryName = "Turkey"},
            new Country() {Id=3 ,CountryName = "Saudi Arabia"},
            new Country() {Id=4 ,CountryName = "Sudan"},
        });


        modelBuilder.Entity<User>().HasData(new User[] {
            new User(){FirstName="Mostafa",
                Id = UserGuidId,
                LastName="Alaa",
                UserName = "Mostafa Alaa",
                Gender = enGender.male,
                Email="test@gmail.com",
                PhoneNumber = "22222222222",
                NationalNo = "12345678910111213",
                CountryId=2,
                BirthDate=new DateTime(1998,11,30),
                Address = "somewhere on th earth",
                ImagePath = "unkown",
            }
        });


        modelBuilder.Entity<Admin>().HasData(new Admin[] {
            new Admin() {
                Id = Guid.NewGuid(),
                UserId =  UserGuidId,
                IsEmployee=true,
                CreatedAt = DateTime.Now,
            }
        });*/

    }
}