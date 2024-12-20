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


namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<GovConnectDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("default")));


        /*builder.Services.AddConfigurationServices(builder.Configuration);*/
        //@@Repository
        builder.Services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
        builder.Services.AddScoped(typeof(IGetAllRepository<>), typeof(GetAllRepository<>));
        builder.Services.AddScoped(typeof(ICreateRepository<>), typeof(CreateRepository<>));
        builder.Services.AddScoped(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        builder.Services.AddScoped(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));

        builder.Services.AddScoped<ICreateCountry, CreateCountryService>();
        builder.Services.AddScoped<IGetCountry, GetCountryService>();
        builder.Services.AddScoped<IGetAllCountries, GetAllCountriesService>();


        //@@ServicePurpose
        builder.Services.AddScoped<ICreateServicePurpose, CreateServicePurposeService>();
        builder.Services.AddScoped<IGetServicePurpose, GetServicePurposeService>();
        builder.Services.AddScoped<IGetAllServicePurpose, GetAllApplicationPurposesService>();
        builder.Services.AddScoped<IUpdateServicePurpose, UpdateServicePurposeService>();
        builder.Services.AddScoped<IDeleteServicePurpose, DeleteApplicationPurposeService>();

        //@@ServiceCategory
        builder.Services.AddScoped<ICreateServiceCategory, CreateServiceCategoryService>();
        builder.Services.AddScoped<IGetServiceCategory, GetServiceCategoryService>();
        builder.Services.AddScoped<IGetAllServiceCategory, GetAllServiceCategoryService>();
        builder.Services.AddScoped<IUpdateServiceCategory, UpdateServiceCategoryService>();
        builder.Services.AddScoped<IDeleteServiceCategory, DeleteServiceCategoryService>();

        builder.Services.AddScoped<IFirstTimeCheckable<CreateLocalDrivingLicenseApplicationRequest>, FirstTimeLocalDrivingLicense>();

        builder.Services.AddScoped<IPendingOrInProgressApplicationStatus, PendingOrInProgressApplicationStatus>();

        builder.Services.AddScoped<ICheckApplicationExistenceService, CheckApplicationExistenceService>();

        builder.Services.AddScoped<IGetLocalDrivingLicenseByUserId, GetLocalDrivingLicneseByUserId>();

        builder.Services.AddScoped<ICreateNewLocalDrivingLicenseApplication, CreateLocalDrivingLicenseApplication>();

        //@@ServiceFees
        builder.Services.AddScoped<ICreateServiceFees, CreateServiceFeesService>();
        builder.Services.AddScoped<IGetServiceFees, GetServiceFeesService>();


        builder.Services.AddScoped<IUpdateCountry, UpdateCountryService>();
        builder.Services.AddScoped<IDeleteCountry, DeleteCountryService>();


        // Add services to the container.
        /*builder.Services.AddAuthorization();
        */
        builder.Services.AddControllers();


        builder.Services.AddIdentity<User, UserRoles>()
            .AddEntityFrameworkStores<GovConnectDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, UserRoles, GovConnectDbContext, Guid>>();

        builder.Services.AddAutoMapper(typeof(GovConnectMapperConfig));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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