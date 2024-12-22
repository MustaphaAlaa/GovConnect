using AutoMapper;
using ModelDTO.CountryDTOs;
using ModelDTO.ApplicationDTOs.User;
using ModelDTO.ApplicationDTOs.Fees;
using ModelDTO.ApplicationDTOs.Category;
using ModelDTO.ApplicationDTOs.Purpose;
using ModelDTO.User;
using Models.ApplicationModels;
using Models.Types;
using Models.Users;
using ModelDTO.ApplicationDTOs.Employee;
using Models.LicenseModels;
using ModelDTO.LicenseDTOs;

namespace Web.Mapper;

/// <summary>
/// Configuration for AutoMapper profiles in the GovConnect application.
/// </summary>
public class GovConnectMapperConfig : Profile
{
    public GovConnectMapperConfig()
    {
        // User mappings
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UpdateUserDTO, UserDTO>().ReverseMap();
        CreateMap<User, RegisterDTO>().ReverseMap();

        // CountryDTOs mappings
        CreateMap<CountryDTO, Country>().ReverseMap();
        CreateMap<UpdateCountryRequest, Country>().ReverseMap();
        CreateMap<CreateCountryRequest, Country>();
        CreateMap<CreateCountryRequest, Country>();

        // ServicePurpose mappings
        CreateMap<ServicePurposeDTO, ServicePurpose>().ReverseMap();
        CreateMap<CreateServicePurposeRequest, ServicePurpose>().ReverseMap();

        // ServiceCategory mappings
        CreateMap<ServiceCategoryDTO, ServiceCategory>().ReverseMap();
        CreateMap<CreateServiceCategoryRequest, ServiceCategory>().ReverseMap();

        // ServiceFees mappings
        CreateMap<ServiceFeesDTO, ServiceCategory>().ReverseMap();

        // DrivingLicenseApplication mappings
        CreateMap<CreateLocalDrivingLicenseApplicationRequest, LocalDrivingLicenseApplication>().ReverseMap();
        CreateMap<CreateInternationalDrivingLicenseApplicationRequest, InternationalDrivingLicenseApplication>().ReverseMap();

        // Application mappings for User
        CreateMap<Application, CreateApplicationRequest>().ReverseMap();
        CreateMap<Application, ApplicationDTOForUser>().ReverseMap();
        CreateMap<Application, UpdateApplicationByUser>().ReverseMap();

        // Application mappings for Employees
        CreateMap<Application, ApplicationDTOForEmployee>().ReverseMap();
        CreateMap<Application, UpdateApplicationByEmployee>().ReverseMap();

        // License mappings
        CreateMap<LocalDrivingLicense, LocalDrivingLicenseDTO>().ReverseMap();
        CreateMap<DetainedLicense, DetainedLicenseDTO>().ReverseMap();
    }
}
