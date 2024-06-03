using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;

namespace EcommerceApi
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoggedDto>().ReverseMap();
            CreateMap<UserDto, UserLoggedDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
