using AutoMapper;
using EcommerceApi.Dto;
using EcommerceApi.Models;

namespace EcommerceApi
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
