using AutoMapper;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;

namespace EcommerceApi
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}
