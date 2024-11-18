using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.App;
using Models.License;
using Models.Test;
using Models.Types;
using Models.Users;

namespace DataConfigurations;

public partial  class DVLDDbContext : IdentityDbContext<User,UserRoles,Guid>
{
    //@@Users
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public DbSet<Driver> Drivers { get; set; }

    //@@Types
    public DbSet<EmployeeType> EmployeeTypes { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<TestType> TestTypes { get; set; }
    public DbSet<LicenseClass> LicenseClasses { get; set; }

    //@@Application
    public DbSet<ApplicationType> ApplicationTypes { get; set; }
    public DbSet<ApplicationFor> ApplicationFor { get; set; }
    public DbSet<ApplicationFees> ApplicationsFees { get; set; }

    //@@Test
    public DbSet<Test> Tests { get; set; }

    public DbSet<TestAppointment> TestAppointments { get; set; }

    //@@License
    public DbSet<License> Licenses { get; set; }

    public DbSet<DetainedLicense> DetainedLicenses { get; set; }

    //@@Application
    public DbSet<Application> Applications { get; set; }
    public DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }
    
    
}

 