using Microsoft.EntityFrameworkCore;
using DataConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;
using Models.Users;
using Web.Mapper;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

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
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "+_+_+_+_+_[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exceptions}") // Write logs to a file

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

        builder.Services.AddConfigurationServices();

        // Add services to the container
        builder.Services.AddControllers();

        // Configure Identity
        builder.Services.AddIdentity<User, UserRoles>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
        })
            .AddEntityFrameworkStores<GovConnectDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, UserRoles, GovConnectDbContext, Guid>>()
            .AddRoleStore<RoleStore<UserRoles, GovConnectDbContext, Guid>>();

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
        app.UseRouting();
        //app.UseCors();

        app.UseHttpLogging();
        Log.Information("----------------------------------Starting application");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
