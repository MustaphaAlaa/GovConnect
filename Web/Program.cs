using Microsoft.EntityFrameworkCore;
using DataConfigurations;
using IRepository;
using IServices.Application.Type;
using IServices.Country;
using IServices.ICountryServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ModelDTO;
using Models;
using Models.Users;
using Repositorties;
using Services.Application.Type;
using Services.CountryServices;
using Web.Mapper;

using IServices.Application.Fees;
using Services.Application;


namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        /*builder.Services.AddConfigurationServices(builder.Configuration);*/
        //@@Repository
        builder.Services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
        builder.Services.AddScoped(typeof(IGetAllRepository<>), typeof(GetAllRepository<>));
        builder.Services.AddScoped(typeof(ICreateRepository<>), typeof(CreateRepository<>));
        /*services.AddScoped(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        services.AddScoped(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));*/

        builder.Services.AddScoped<ICreateCountry, CreateCountryService>();
        builder.Services.AddScoped<IGetCountry, GetCountryService>();
        builder.Services.AddScoped<IGetAllCountries, GetAllCountriesService>();

        //@@ApplicationType
        builder.Services.AddScoped<ICreateApplicationType, CreateApplicationTypeService>();
        builder.Services.AddScoped<IGetApplicationType, GetApplicationTypeService>();
        builder.Services.AddScoped<IGetAllApplicationTypes, GetAllApplicationTypesService>();
        builder.Services.AddScoped<IUpdateApplicationType, UpdateApplicationTypeService>();
        builder.Services.AddScoped<IDeleteApplicationType, DeleteApplicationTypeService>();

        //@@ApplicationFor
        builder.Services.AddScoped<ICreateApplicationType, CreateApplicationTypeService>();
        builder.Services.AddScoped<IGetApplicationType, GetApplicationTypeService>();
        builder.Services.AddScoped<IGetAllApplicationTypes, GetAllApplicationTypesService>();
        builder.Services.AddScoped<IUpdateApplicationType, UpdateApplicationTypeService>();
        builder.Services.AddScoped<IDeleteApplicationType, DeleteApplicationTypeService>();

        //@@ApplicationFees
        //builder.Services.AddScoped<ICreateApplicationFees, CreateApplicationFeesService>();
        //builder.Services.AddScoped<IGetApplicationFees, GetApplicationFeesService>();


        builder.Services.AddScoped<IUpdateCountry, UpdateCountryService>();
        builder.Services.AddScoped<IDeleteCountry, DeleteCountryService>();

        // Add services to the container.
        /*builder.Services.AddAuthorization();
        */
        builder.Services.AddControllers();


        builder.Services.AddDbContext<DVLDDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

        builder.Services.AddIdentity<User, UserRoles>()
            .AddEntityFrameworkStores<DVLDDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, UserRoles, DVLDDbContext, Guid>>();

        builder.Services.AddAutoMapper(typeof(DVLDMapperConfig));

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