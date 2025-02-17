using IServices.IApplicationServices.IPurpose;
using IServices.ICountryServices;
using IServices.IApplicationServices.Category;
using Services.ApplicationServices.Purpose;
using Services.CountryServices;
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
using Services.LDLApplicationsAllowedToRetakeATestServices;
using IServices.ITests.ITest;
using Services.TestServices;
using IRepository.ITVFs;
using Repositorties.TVFs;
using IServices.IValidators.BookingValidators;
using Services.BookingServices.Validators;
using IServices.ILicenseServices;
using Services.LicensesServices;
using Repositorties.GenericRepostiory;
using Repositorties.TestRepos;
using Services.ApplicationServices.ServiceCategoryApplications.LocalDrivingLicenseApplications;
using IRepository.IGenericRepositories;
using IRepository.ITestRepos;
using IRepository.ISPs.IAppointmentProcedures;
using Repositorties.SPs.AppointmentReps;
using IServices.IApplicationServices.IServiceCategoryApplications.IRetakeTestApplication;
using IServices.ILDLApplicationsAllowedToRetakeATestServices;
using DefaultNamespace;
using IServices.IDriverServices;
using IServices.IValidators.DriverValidators;
using Services.DriverServices;
using Services.DriverServices.Validators;
using Services.SubscriptionsServices.Tests;
using IServices.ILicenseClassServices;
using IServices.IUserServices;
using Services.UsersServices;

namespace Web;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services)
    {
 
        // Register StoredProcedure 
        services.AddScoped<ISP_InsertAppointment, SPInsertAppointment>();
        services.AddScoped<ISP_MarkExpiredAppointmentsAsUnavailable, SPMarkExpiredAppointmentsAsUnavailable>();

        // Register Function
        services.AddScoped<ITVF_GetTestTypeDayTimeInterval, TVFGetTestTypeDayTimeInterva>();
        services.AddScoped<ITVF_GetAvailableDays, TVFGetAvailableDays>();
        services.AddScoped<ITVF_GetTestResult, TVFGetTestResult>();
        services.AddScoped<ITVF_GetTestResultForABookingId, TVFGetTestResultForABookingId>();
        services.AddScoped<ITVF_GetLDLAppsAllowedToRetakATest, TVFGetLDLAppsAllowedToRetakATest>();
        services.AddScoped<ITestTypePassedChecker, TestTypePassedChecker>();


        // Register Repositories
        services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
        services.AddScoped<IGetRepository<Application>, GetRepository<Application>>();
        services.AddScoped(typeof(IGetAllRepository<>), typeof(GetAllRepository<>));
        services.AddScoped(typeof(ICreateRepository<>), typeof(CreateRepository<>));
        services.AddScoped(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        services.AddScoped(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));

        // Register Country Services
        services.AddScoped<ICreateCountry, CreateCountryService>();
        services.AddScoped<IGetCountry, GetCountryService>();
        services.AddScoped<IGetAllCountries, GetAllCountriesService>();
        services.AddScoped<IUpdateCountry, UpdateCountryService>();
        services.AddScoped<IDeleteCountry, DeleteCountryService>();

        // Register ServicePurpose Services
        services.AddScoped<ICreateServicePurpose, CreateServicePurposeService>();
        services.AddScoped<IGetServicePurpose, GetServicePurposeService>();
        services.AddScoped<IGetAllServicePurpose, GetAllApplicationPurposesService>();
        services.AddScoped<IUpdateServicePurpose, UpdateServicePurposeService>();
        services.AddScoped<IDeleteServicePurpose, DeleteApplicationPurposeService>();

        // Register ServiceCategory Services
        services.AddScoped<ICreateServiceCategory, CreateServiceCategoryService>();
        services.AddScoped<IGetServiceCategory, GetServiceCategoryService>();
        services.AddScoped<IGetAllServiceCategory, GetAllServiceCategoryService>();
        services.AddScoped<IUpdateServiceCategory, UpdateServiceCategoryService>();
        services.AddScoped<IDeleteServiceCategory, DeleteServiceCategoryService>();

        // Register Local Driving License Services
        services.AddScoped<IFirstTimeApplicationCheckable<CreateLocalDrivingLicenseApplicationRequest>, FirstTimeLocalDrivingLicense>();
        services.AddScoped<IPendingOrInProgressApplicationStatus, PendingOrInProgressApplicationStatus>();
        services.AddScoped<ICheckApplicationExistenceService, CheckApplicationExistenceService>();
        services.AddScoped<IGetLocalDrivingLicenseByUserId, GetLocalDrivingLicneseByUserId>();
        services.AddScoped<INewLocalDrivingLicenseApplicationCreator, LocalDrivingLicenseApplicationCreator>();
        services.AddScoped<ICreateLocalDrivingLicenseApplicationOrchestrator, CreateLocalLicenseApplicationOrchestrator>();
        services.AddScoped<INewLocalDrivingLicenseApplicationValidator, NewLocalDrivingLicenseApplicationValidator>();
        services.AddScoped<IRenewLocalDrivingLicenseApplicationValidator, RenewLocalDrivingLicenseApplicationValidator>();
        services.AddScoped<IReplacementForDamageLocalDrivingLicenseApplicationValidator, ReplacementForDamageLocalDrivingLicenseApplicationValidator>();
        services.AddScoped<IReplacementForLostLocalDrivingLicenseApplicationValidator, ReplacementForLostLocalDrivingLicenseApplicationValidator>();
        services.AddScoped<IReleaseLocalDrivingLicenseApplicationValidator, ReleaseLocalDrivingLicenseApplicationValidator>();
        services.AddScoped<IGetDetainLicense, GetDetainedLicense>();
        services.AddScoped<ILocalLicenseRetrieveService, LocalDrivingLicenseRetrievalService>();
        services.AddScoped<ILocalDrivingLicenseApplicationRetrieve, GetLocalDriveLiecenseApplication>();
        services.AddScoped<ICreateRetakeTestApplicationValidation, CreateRetakeTestApplicationValidator>();

        //Register Local Driving License Services
        services.AddScoped<ILocalDrivingLicenseCreationService, LocalDrivingLicenseCreatorService>();
        services.AddScoped<ILocalDrivingLicenseUpdateService, LocalDrivingLicenseUpdateService>();

        // Register Application Services
        services.AddScoped<ICreateApplicationService, CreateApplicationService>();
        services.AddScoped<ICreateApplicationEntity, CreateApplicationEntity>();

        // Register ServiceFees Services
        services.AddScoped<ICreateServiceFees, CreateServiceFeesService>();
        services.AddScoped<IUpdateServiceFees, UpdateServiceFeesService>();
        services.AddScoped<IDeleteServiceFees, DeleteServiceFeesService>();
        services.AddScoped<IServiceFeeRetrieverService, GetServiceFeesService>();

        //Register Driver Services
        services.AddScoped<IDriverRetrieveService, DriverRetrievalService>();
        services.AddScoped<IDriverCreationValidator, DriverCreatorValidator>();
        services.AddScoped<IDriverCreatorService, DriverCreatorService>();
        services.AddScoped<IDriverUpdateService, DriverUpdateService>();

        // Register User Services
        services.AddScoped<IUserRetrieveService, UserRetrieveServices>();
        services.AddScoped<IUserRegistrationService, UserRegistrationValidationService>();

        // Register License Class Services
        services.AddScoped<ILicenseClassRetrieve, LicenseClassRetrieve>();


        // Register Tests Services
        services.AddScoped<ITestTypeRetrievalService, GetTestTypesService>();
        services.AddScoped<IAsyncAllTestTypesRetrieverService, GetAllTestTypesService>();
        services.AddScoped<ITestCreationService, TestCreatorService>();
        services.AddScoped<ICreateTestValidator, CreateTestValidator>();
        services.AddScoped<ITestResultInfoRetrieve, TestResultInfoRetrieve>();

        // Register Appointment Service
        services.AddScoped<ICreateAppointmentService, CreateAppointmentsService>();
        services.AddScoped<IGetAppointmentService, GetAppointmentService>();
        services.AddScoped<IGetAllAppointmentsService, GetAllAppointmentsService>();
        services.AddScoped<IGetTimeIntervalService, GetTimeIntervalService>();
        services.AddScoped<IGetAllTimeIntervalService, GetAllTimeIntervalService>();

        // Register Validators Service
        services.AddScoped<IDateValidator, CreateDateValidator>();
        services.AddScoped<ITestTypeValidator, TestTypeValidator>();

        // Register Booking Service
        services.AddScoped<IFirstTimeBookingAnAppointmentValidation, FirstTimeBookingAnAppointment>();
        services.AddScoped<IBookingCreationValidators, BookingCreationValidator>();
        services.AddScoped<ITestTypeOrder, BookingTestTypeOrder>();
        services.AddScoped<IBookingRetrieveService, BookingRetrivalService>();
        services.AddScoped<ICreateBookingService, BookingAnAppointmentService>();

        //Register RetakeTest Service
        services.AddScoped<ILDLTestRetakeApplicationCreator, LDLTestRetakeApplicationCreator>();
        services.AddScoped<IRetakeTestApplicationBookingValidator, RetakeTestApplicationBookingValidator>();
        services.AddScoped<IRetakeTestApplicationCreation, RetakeTestApplicationCreateor>();
        services.AddScoped<IRetakeTestApplicationRetriever, RetakeTestApplicationRetriever>();
        services.AddScoped<ILDLTestRetakeApplicationUpdater, LDLTestRetakeApplicationUpdater>();

        services.AddScoped<ILDLTestRetakeApplicationCreationValidator, LDLApplicationAllowedToRetakeTestCreationValidator>();
        services.AddScoped<ILDLTestRetakeApplicationRetrieve, LDLTestRetakeApplicationRetrieval>();
        services.AddScoped<ILDLTestRetakeApplicationSubscriber, LDLTestRetakeApplicationSubscriber>();

        services.AddScoped<IFinalTestSubscriber, FinalTestPassedSubscriber>();

        services.AddScoped<IAppointmentUpdateService, AppointmentUpdateService>();


        return services;
    }
}