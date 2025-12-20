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
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;

    public DbSet<Driver> Drivers { get; set; } = null!;

    //@@Types
    public DbSet<EmployeeType> EmployeeTypes { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<LicenseClass> LicenseClasses { get; set; } = null!;

    //@@DrivingLicenseApplication
    public DbSet<ServicePurpose> ServicesPurposes { get; set; } = null!;
    public DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
    public DbSet<ServiceFees> ServicesFees { get; set; } = null!;

    //@@Test
    public DbSet<Test> Tests { get; set; } = null!;
    public DbSet<TestType> TestTypes { get; set; } = null!;
    public DbSet<RetakeTestApplication> RetakeTestApplications { get; set; } = null!;
    public DbSet<LDLApplicationsAllowedToRetakeATest> LDLApplicationsAllowedToRetakeATests { get; set; } = null!;
    //@@Appointments
    public DbSet<Appointment> TestAppointments { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;

    //@@LicensesServices
    public DbSet<LocalDrivingLicense> LocalDrivingLicenses { get; set; } = null!;
    public DbSet<InternationalDrivingLicense> InternationalDrivingLicenses { get; set; } = null!;
    public DbSet<LicenseType> LicenseTypes { get; set; } = null!;
    public DbSet<DetainedLicense> DetainedLicenses { get; set; } = null!;

    //@@DrivingLicenseApplication
    public DbSet<Application> Applications { get; set; } = null!;
    public DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; } = null!;
    public DbSet<InternationalDrivingLicenseApplication> InternationalDrivingLicenseApplications { get; set; } = null!;
}

