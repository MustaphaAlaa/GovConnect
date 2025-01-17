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
using IServices.ITimeIntervalService;
using IServices.IValidators;
using Services.AppointmentsService;
using Services.TimeIntervalServices;
using IServices.IValidtors.ILocalDrivingLicenseApplications;
using Services.ApplicationServices.Validators;
using DataConfigurations.TVFs.ITVFs;
using IServices.IBookingServices;
using Services.BookingServices;
using IServices.ITests.ITestTypes;
using Services.TestTypeServices;
using IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;
using Services.LDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using Services.TestServices;
using Microsoft.Extensions.Configuration;
using IRepository.ITVFs;
using Repositorties.TVFs;
using Repositorties.SPs;
using IRepository.ISPs;
using Models.Tests;

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
            options.UseSqlServer(builder.Configuration.GetConnectionString("default")), ServiceLifetime.Scoped);




        /////////   builder.Services.AddScoped<ICreateRepository<LDLApplicationsAllowedToRetakeATest>, CreateRepository<LDLApplicationsAllowedToRetakeATest>>();
        /////// Other service registrations

        // Register StoredProcedure 
        builder.Services.AddScoped<ISP_InsertAppointment, SPInsertAppointment>();

        // Register Function
        builder.Services.AddScoped<ITVF_GetTestTypeDayTimeInterval, TVFGetTestTypeDayTimeInterva>();
        builder.Services.AddScoped<ITVF_GetAvailableDays, TVFGetAvailableDays>();
        builder.Services.AddScoped<ITVF_GetTestResult, TVFGetTestResult>();
        builder.Services.AddScoped<ITVF_GetTestResultForABookingId, TVFGetTestResultForABookingId>();
        builder.Services.AddScoped<ITVF_GetLDLAppsAllowedToRetakATest, TVFGetLDLAppsAllowedToRetakATest>();


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
        builder.Services.AddScoped<INewLocalDrivingLicenseApplicationCreator, LocalDrivingLicenseApplicationCreator>();
        builder.Services.AddScoped<ICreateLocalDrivingLicenseApplicationOrchestrator, CreateLocalLicenseApplicationOrchestrator>();
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
        builder.Services.AddScoped<IUpdateServiceFees, UpdateServiceFeesService>();
        builder.Services.AddScoped<IDeleteServiceFees, DeleteServiceFeesService>();
        builder.Services.AddScoped<IServiceFeeRetrieverService, GetServiceFeesService>();


        // Register Tests Services
        builder.Services.AddScoped<ITestTypeRetrievalService, GetTestTypesService>();
        builder.Services.AddScoped<IAsyncAllTestTypesRetrieverService, GetAllTestTypesService>();
        builder.Services.AddScoped<ITestCreationService, TestCreatorService>();
        builder.Services.AddScoped<ICreateTestValidator, CreateTestValidator>();

        // Register Appointment Service
        builder.Services.AddScoped<ICreateAppointmentService, CreateAppointmentsService>();
        builder.Services.AddScoped<IGetAppointmentService, GetAppointmentService>();
        builder.Services.AddScoped<IGetAllAppointmentsService, GetAllAppointmentsService>();
        builder.Services.AddScoped<IGetTimeIntervalService, GetTimeIntervalService>();
        builder.Services.AddScoped<IGetAllTimeIntervalService, GetAllTimeIntervalService>();

        // Register Validators Service
        builder.Services.AddScoped<IDateValidator, CreateDateValidator>();
        builder.Services.AddScoped<ITestTypeValidator, TestTypeValidator>();

        // Register Booking Service
        builder.Services.AddScoped<IFirstTimeBookingAnAppointment, FirstTimeBookingAnAppointment>();

        //Register RetakeTest Service
        builder.Services.AddScoped<ILDLTestRetakeApplicationCreator, LDLTestRetakeApplicationCreator>();
        builder.Services.AddScoped<ILDLTestRetakeApplicationCreationValidator, LDLTestRetakeApplicationValidator>();


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
