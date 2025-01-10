using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ApplicationModels;
using Models.LicenseModels;
using Models.Tests;
using Models.Types;
using Models.Users;
using Models.Countries;
using Models.Applications;

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
    public DbSet<LicenseClass> LicenseClasses { get; set; }

    //@@DrivingLicenseApplication
    public DbSet<ServicePurpose> ServicesPurposes { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceFees> ServicesFees { get; set; }

    //@@Test
    public DbSet<Test> Tests { get; set; }
    public DbSet<TestType> TestTypes { get; set; }
    public DbSet<RetakeTestApplication> RetakeTestApplications { get; set; }

    //@@Appointments
    public DbSet<Appointment> TestAppointments { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    //@@LicensesServices
    public DbSet<LocalDrivingLicense> LocalDrivingLicenses { get; set; }
    public DbSet<InternationalDrivingLicense> InternationalDrivingLicenses { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<DetainedLicense> DetainedLicenses { get; set; }

    //@@DrivingLicenseApplication
    public DbSet<Application> Applicataions { get; set; }
    public DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }
    public DbSet<InternationalDrivingLicenseApplication> InternationalDrivingLicenseApplications { get; set; }


}

