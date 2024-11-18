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

        builder.Services.AddScoped<ICreateApplicationType, CreateApplicationTypeService>();

        
        /*services.AddScoped<IUpdateCountry, UpdateCountryService>();
        services.AddScoped<IDeleteCountry, DeleteCountryService>();*/

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