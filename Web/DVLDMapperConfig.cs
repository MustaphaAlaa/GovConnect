using AutoMapper;
using ModelDTO;
using ModelDTO.User;
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
    }
}