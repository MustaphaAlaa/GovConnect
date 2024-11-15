/*using IRepository;
using IServices;
using IServices.Country;
using IServices.ICountryServices;
using Models.Types;
using Repositorties;
using Services.CountryServices;

namespace Web.ConfigurationServices;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //@@Repository
        services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
        services.AddScoped(typeof(IGetAllRepository<>), typeof(GetAllRepository<>));
        services.AddScoped(typeof(ICreateRepository<>), typeof(CreateRepository<>));
        /*services.AddScoped(typeof(IUpdateRepository<>), typeof(UpdateRepository<>));
        services.AddScoped(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));#1#

        services.AddScoped<ICreateCountry, CreateCountryService>();
        services.AddScoped<IGetCountry, GetCountryService>();
        services.AddScoped<IGetAllCountries, GetAllCountriesService>();
        /*services.AddScoped<IUpdateCountry, UpdateCountryService>();
        services.AddScoped<IDeleteCountry, DeleteCountryService>();#1#

        return services;
    }
}*/