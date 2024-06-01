using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;

namespace EcommerceApi
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoggedDto>().ReverseMap();
            CreateMap<UserDto, UserLoggedDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
