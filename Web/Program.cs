using Microsoft.EntityFrameworkCore;
using DataConfigurations;
using IRepository;
using IServices.IApplicationServices.IPurpose;
using IServices.Country;
using IServices.IApplicationServices.Category;
using IServices.ICountryServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;
using Models.Users;
using Repositorties;
using Services.ApplicationServices.Purpose;
using Services.CountryServices;
using Web.Mapper;
using IServices.IApplicationServices.Fees;
using Services.ApplicationServices.Fees;
using Services.ApplicationServices.For;
using IServices.IApplicationServices.User;
using ModelDTO.ApplicationDTOs.User;
using Services.ApplicationServices.Services.UserAppServices.IsFirstTime;
using Services.ApplicationServices.ServiceCategoryApplications;
using Services.ApplicationServices.Services.UserAppServices;
using IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;
using GovConnect.IServices.ILicensesServices.IDetainLicenses;


namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure DbContext
        builder.Services.AddDbContext<GovConnectDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

        // Register Repositories
        builder.Services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
        builder.Services.AddScoped(typeof(IGetAllRepository<>), typeof(GetAllRepository<>));
        builder.Services.AddScoped(typeof(ICreateRepository<>), typeof(CreateRepository<>));
        builder.Services.AddScoped(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        builder.Services.AddScoped(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));

        // Register Country Services
        builder.Services.AddScoped<ICreateCountry, CreateCountryService>();
        builder.Services.AddScoped<IGetCountry, GetCountryService>();
        builder.Services.AddScoped<IGetAllCountries, GetAllCountriesService>();
        builder.Services.AddScoped<IUpdateCountry, UpdateCountryService>();
        builder.Services.AddScoped<IDeleteCountry, DeleteCountryService>();

        // Register ServicePurpose Services
        builder.Services.AddScoped<ICreateServicePurpose, CreateServicePurposeService>();
        builder.Services.AddScoped<IGetServicePurpose, GetServicePurposeService>();
        builder.Services.AddScoped<IGetAllServicePurpose, GetAllApplicationPurposesService>();
        builder.Services.AddScoped<IUpdateServicePurpose, UpdateServicePurposeService>();
        builder.Services.AddScoped<IDeleteServicePurpose, DeleteApplicationPurposeService>();

        // Register ServiceCategory Services
        builder.Services.AddScoped<ICreateServiceCategory, CreateServiceCategoryService>();
        builder.Services.AddScoped<IGetServiceCategory, GetServiceCategoryService>();
        builder.Services.AddScoped<IGetAllServiceCategory, GetAllServiceCategoryService>();
        builder.Services.AddScoped<IUpdateServiceCategory, UpdateServiceCategoryService>();
        builder.Services.AddScoped<IDeleteServiceCategory, DeleteServiceCategoryService>();

        // Register Local Driving License Services
        builder.Services.AddScoped<IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest>, FirstTimeLocalDrivingLicense>();
        builder.Services.AddScoped<IPendingOrInProgressApplicationStatus, PendingOrInProgressApplicationStatus>();
        builder.Services.AddScoped<ICheckApplicationExistenceService, CheckApplicationExistenceService>();
        builder.Services.AddScoped<IGetLocalDrivingLicenseByUserId, GetLocalDrivingLicneseByUserId>();
        builder.Services.AddScoped<ICreateNewLocalDrivingLicenseApplication, CreateLocalDrivingLicenseApplication>();
        builder.Services.AddScoped<ICreateLocalDrivingLicenseApplicationService, CreateLocalDrivingLicenseApplicationService>();
        builder.Services.AddScoped<INewLocalDrivingLicenseApplicationValidator, NewLocalDrivingLicenseApplicationValidator>();
        builder.Services.AddScoped<IRenewLocalDrivingLicenseApplicationValidator, RenewLocalDrivingLicenseApplicationValidator>();
        builder.Services.AddScoped<IReplacementForDamageLocalDrivingLicenseApplicationValidator, ReplacementForDamageLocalDrivingLicenseApplicationValidator>();
        builder.Services.AddScoped<IReplacementForLostLocalDrivingLicenseApplicationValidator, ReplacementForLostLocalDrivingLicenseApplicationValidator>();
        builder.Services.AddScoped<IReleaseLocalDrivingLicenseApplicationValidator, ReleaseLocalDrivingLicenseApplicationValidator>();
        builder.Services.AddScoped<IGetDetainLicense, GetDetainedLicense>();

        // Register Application Services
        builder.Services.AddScoped<ICreateApplicationService, CreateApplicationService>();
        builder.Services.AddScoped<ICreateApplicationEntity, CreateApplicationEntity>();

        // Register ServiceFees Services
        builder.Services.AddScoped<ICreateServiceFees, CreateServiceFeesService>();
        builder.Services.AddScoped<IGetServiceFees, GetServiceFeesService>();

        // Add services to the container
        builder.Services.AddControllers();

        // Configure Identity
        builder.Services.AddIdentity<User, UserRoles>()
            .AddEntityFrameworkStores<GovConnectDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, UserRoles, GovConnectDbContext, Guid>>();

        // Configure AutoMapper
        builder.Services.AddAutoMapper(typeof(GovConnectMapperConfig));

        // Configure Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
