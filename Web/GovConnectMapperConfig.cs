using AutoMapper;
using ModelDTO.CountryDTOs;
using ModelDTO.ApplicationDTOs.User;
using ModelDTO.ApplicationDTOs;
using ModelDTO.ApplicationDTOs.Fees;
using ModelDTO.ApplicationDTOs.For;
using ModelDTO.ApplicationDTOs.Type;
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

        //@@ApplicationType
        CreateMap<ApplicationTypeDTO, ApplicationType>().ReverseMap();
        CreateMap<CreateApplicationTypeRequest, ApplicationType>().ReverseMap();

        //@@ApplicationFor
        CreateMap<ApplicationForDTO, ApplicationFor>().ReverseMap();
        CreateMap<CreateApplicationForRequest, ApplicationFor>().ReverseMap();

        //@@ApplicationFees
        CreateMap<ApplicationFeesDTO, ApplicationFor>().ReverseMap();

        //@@LicenseApplication
        //@@User
        CreateMap<LicenseApplication, CreateApplicationRequest>().ReverseMap();
        CreateMap<LicenseApplication, ApplicationDTOForUser>().ReverseMap();
        CreateMap<LicenseApplication, UpdateApplicationByUser>().ReverseMap();
        //@@Employees
        CreateMap<LicenseApplication, ApplicationDTOForEmployee>().ReverseMap();
        CreateMap<LicenseApplication, UpdateApplicationByEmployee>().ReverseMap();

    }
}