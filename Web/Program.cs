using Microsoft.EntityFrameworkCore;
using DataConfigurations;
using IRepository;
using IServices.IApplicationServices.IPurpose;
using IServices.ICountryServices;
using IServices.IApplicationServices.Category;
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
using IServices.IAppointments;
using Models.ApplicationModels;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using IServices.ITests;
using IServices.ITimeIntervalService;
using IServices.IValidators;
using Services.AppointmentsService;
using Services.TestServices;
using Services.TimeIntervalServices;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Services.ApplicationServices.Validators;
using DataConfigurations.TVFs.ITVFs;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Serilog

        builder.Host.UseSerilog();
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            // .WriteTo.Console() // Write logs to the console
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "+_+_+_+_+_[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}") // Write logs to a file

            .CreateLogger();

        // Use Serilog as the logging provider  
        builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider serviceProvider,
            LoggerConfiguration loggerConfiguration) =>
        {
            // Read Serilog configuration from appsettings.json
            loggerConfiguration.ReadFrom.Configuration(context.Configuration)
            // Read Serilog configuration from the registered services 
            .ReadFrom.Services(serviceProvider);
        });

        // Configure DbContext
        builder.Services.AddDbContext<GovConnectDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

        // Register StoredProcedure 
        builder.Services.AddScoped<ISP_InsertAppointment, GovConnectDbContext>();

        // Register Function
        builder.Services.AddScoped<ITVF_GetTestTypeDayTimeInterval, GovConnectDbContext>();
        builder.Services.AddScoped<ITVF_GetAvailableDays, GovConnectDbContext>();

        // Register Repositories
        builder.Services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
        builder.Services.AddScoped<IGetRepository<Application>, GetRepository<Application>>();
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
        builder.Services.AddScoped<IFirstTimeApplicationCheckable<CreateLocalDrivingLicenseApplicationRequest>, FirstTimeLocalDrivingLicense>();
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

        //Register Tests Services
        builder.Services.AddScoped<IGetTestTypeService, GetTestTypesService>();
        builder.Services.AddScoped<IGetAllTestTypesService, GetAllTestTypesService>();

        // Register Appointment Service
        builder.Services.AddScoped<ICreateAppointmentService, CreateAppointmentsService>();
        builder.Services.AddScoped<IGetAppointmentService, GetAppointmentService>();
        builder.Services.AddScoped<IGetAllAppointmentsService, GetAllAppointmentsService>();
        builder.Services.AddScoped<IGetTimeIntervalService, GetTimeIntervalService>();
        builder.Services.AddScoped<IGetAllTimeIntervalService, GetAllTimeIntervalService>();

        // Register Validators Service
        builder.Services.AddScoped<IDateValidator, CreateDateValidator>();
        builder.Services.AddScoped<ITestTypeValidator, TestTypeValidator>();


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

        builder.Services.AddHttpLogging(opt =>
        {
            opt.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders
                                | HttpLoggingFields.ResponseBody | HttpLoggingFields.RequestBody;

        });

        var app = builder.Build();




        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpLogging();
        Log.Information("----------------------------------Starting application");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
