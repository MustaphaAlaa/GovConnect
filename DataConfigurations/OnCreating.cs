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
using Microsoft.Extensions.Options;
using ModelDTO.TestsDTO;
using System.Reflection;

namespace DataConfigurations;

public partial class GovConnectDbContext : IdentityDbContext<User, UserRoles, Guid>
{

    IConfiguration _configuration;

    public GovConnectDbContext(DbContextOptions<GovConnectDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;

    }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovConnectDbContext).Assembly);

        modelBuilder.Entity<AvailableDay>().HasNoKey();

        modelBuilder.HasDbFunction(typeof(GovConnectDbContext)
            .GetMethod(nameof(GetAvailableDays), new[] { typeof(int) }))
            .HasName("GetAvailableDays");

        modelBuilder.HasDbFunction(typeof(GovConnectDbContext)
            .GetMethod(nameof(GetLDLAppsAllowedToRetakATest), new[] { typeof(int), typeof(int) }))
            .HasName("GetLDLAppsAllowedToRetakATest");

        modelBuilder.HasDbFunction(typeof(GovConnectDbContext)
            .GetMethod(nameof(GetPassedTest), new[] { typeof(int), typeof(int) }))
            .HasName("GetPassedTest");


        modelBuilder.HasDbFunction(typeof(GovConnectDbContext)
            .GetMethod(nameof(GetTestResult), new[] { typeof(int) }))
            .HasName("GetTestResult");


        modelBuilder.HasDbFunction(typeof(GovConnectDbContext)
         .GetMethod(nameof(GetTestResultForABookingId), new[] { typeof(int) }))
         .HasName("GetTestResultForABookingId");

        modelBuilder.HasDbFunction(typeof(GovConnectDbContext)
            .GetMethod(nameof(GetTestTypeDayTimeInterval), new[] { typeof(int), typeof(DateOnly) }))
            .HasName("GetTestTypeDayTimeInterval");


    }

    // Define the TVF method
    public IQueryable<AvailableDay> GetAvailableDays(
        int testId)
    {
        return FromExpression(() => GetAvailableDays(testId));
    }

    public IQueryable<LDLApplicationsAllowedToRetakeATestDTO> GetLDLAppsAllowedToRetakATest(
        int ldlAppId, int testTypeId)
    {
        return FromExpression(() => GetLDLAppsAllowedToRetakATest(ldlAppId, testTypeId));
    }

    public IQueryable<TestDTO> GetPassedTest(
    int ldlApplicationId, int testId)
    {
        return FromExpression(() => GetPassedTest(ldlApplicationId, testId));
    }

    public IQueryable<TestDTO> GetTestResult(int testId)
    {
        return FromExpression(() => GetTestResult(testId));
    }

    public IQueryable<TestDTO> GetTestResultForABookingId(
        int bookingId)
    {
        return FromExpression(() => GetTestResultForABookingId(bookingId));
    }


    public IQueryable<TestDTO> GetTestTypeDayTimeInterval(
       int testTypeId, DateOnly day)
    {
        return FromExpression(() => GetTestTypeDayTimeInterval(testTypeId, day));
    }










    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("default"));
    }
}