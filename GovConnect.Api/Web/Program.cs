using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Serilog;
using DataConfigurations;
using Models;
using Models.Users;
using Web.Mapper;
using Microsoft.AspNetCore.HttpLogging;


namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:is"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:aud"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
                };
            });


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("JWT", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
            });
        });


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
            .AddUserStore<UserStore<User, UserRoles, GovConnectDbContext, Guid>>()
            .AddRoleStore<RoleStore<UserRoles, GovConnectDbContext, Guid>>()
            .AddDefaultTokenProviders();

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
        //app.UseRouting();
        //app.UseCors();

        app.UseHttpLogging();
        Log.Information("----------------------------------Starting application");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {


            Log.Information("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Log.Information($"{context?.User?.Identity?.IsAuthenticated}");
            Log.Information("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            await next();
            Log.Information($"{context?.User?.Identity?.IsAuthenticated}");
            Log.Information("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");

        });

        app.Use(async (context, next) =>
        {
            Log.Information("##############################################################");
            Log.Information("##############################################################");
            Log.Information("##############################################################");
            Log.Information("##############################################################");
            Log.Information("##############################################################");
            Log.Information($"Request: {context.Request.Method} {context.Request.Path}");

            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                Log.Information($"Authorization Header: {context.Request.Headers["Authorization"]}");
            }
            else
            {
                Log.Information("Authorization header is missing.");
            }
            Log.Information("##############################################################");
            Log.Information("##############################################################");
            Log.Information("##############################################################");
            Log.Information("##############################################################");

            await next();
        });



        app.MapControllers();
        app.Run();
    }
}
