using AutoMapper;
using ModelDTO;
using ModelDTO.Application.Fees;
using ModelDTO.Application.For;
using ModelDTO.Application.Type;
using ModelDTO.User;
using Models.ApplicationModels;
using Models.Types;
using Models.Users;

namespace Web.Mapper;

public class DVLDMapperConfig : Profile
{
    public DVLDMapperConfig()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UpdateUserDTO, UserDTO>().ReverseMap();
        CreateMap<User, RegisterDTO>().ReverseMap();


        //@@Country
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

    }
}