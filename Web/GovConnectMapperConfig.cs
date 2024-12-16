using AutoMapper;
using ModelDTO.CountryDTOs;
using ModelDTO.ApplicationDTOs.User;
using ModelDTO.ApplicationDTOs;
using ModelDTO.ApplicationDTOs.Fees;
using ModelDTO.ApplicationDTOs.Category;
using ModelDTO.ApplicationDTOs.Purpose;
using ModelDTO.User;
using Models.ApplicationModels;
using Models.Types;
using Models.Users;
using ModelDTO.ApplicationDTOs.Employee;

namespace Web.Mapper;

public class GovConnectMapperConfig : Profile
{
    public GovConnectMapperConfig()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UpdateUserDTO, UserDTO>().ReverseMap();
        CreateMap<User, RegisterDTO>().ReverseMap();


        //@@CountryDTOs
        CreateMap<CountryDTO, Country>().ReverseMap();
        CreateMap<UpdateCountryRequest, Country>().ReverseMap();
        CreateMap<CreateCountryRequest, Country>();
        CreateMap<Country, CreateCountryRequest>();

        //@@ApplicationPurpose
        CreateMap<ApplicationPurposeDTO, ApplicationPurpose>().ReverseMap();
        CreateMap<CreateApplicationPurposeRequest, ApplicationPurpose>().ReverseMap();

        //@@ServiceCategory
        CreateMap<ServiceCategoryDTO, ServiceCategory>().ReverseMap();
        CreateMap<CreateServiceCategoryRequest, ServiceCategory>().ReverseMap();

        //@@ServiceFees
        CreateMap<ServiceFeesDTO, ServiceCategory>().ReverseMap();

        //@@DrivingLicenseApplication
        //@@User
        CreateMap<Application, CreateApplicationRequest>().ReverseMap();
        CreateMap<Application, ApplicationDTOForUser>().ReverseMap();
        CreateMap<Application, UpdateApplicationByUser>().ReverseMap();
        //@@Employees
        CreateMap<Application, ApplicationDTOForEmployee>().ReverseMap();
        CreateMap<Application, UpdateApplicationByEmployee>().ReverseMap();

    }
}