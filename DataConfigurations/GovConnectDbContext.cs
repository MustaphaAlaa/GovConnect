using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ApplicationModels;
using Models.LicenseModels;
using Models.Test;
using Models.Types;
using Models.Users;

namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
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

    //@@DrivingLicenseApplication
    public DbSet<ServicePurpose> ServicesPurposes { get; set; }
    public DbSet<ServiceCategory> ApplicationFor { get; set; }
    public DbSet<ServiceFees> ApplicationsFees { get; set; }

    //@@Test
    public DbSet<Test> Tests { get; set; }

    public DbSet<TestAppointment> TestAppointments { get; set; }

    //@@LicensesServices
    public DbSet<LocalDrivingLicense> LocalLicenses { get; set; }
    public DbSet<InternationalDrivingLicense> InternationalLicenses { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<DetainedLicense> DetainedLicenses { get; set; }

    //@@DrivingLicenseApplication
    public DbSet<Application> LicenseApplications { get; set; }
    public DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }
    public DbSet<InternationalDrivingLicenseApplication> InternationalDrivingLicenseApplication { get; set; }


}

