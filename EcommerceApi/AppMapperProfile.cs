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
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<FavoriteProduct, FavoriteProductDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<FeatureCategory, FeatureCategoryDto>().ReverseMap();
            CreateMap<FeatureProduct, FeatureProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderProduct, OrderProductDto>().ReverseMap();
            CreateMap<OrderHistory, OrderHistoryDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<PaymentCard, PaymentCardDto>().ReverseMap();
            CreateMap<PrincipalProduct, PrincipalProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoggedDto>().ReverseMap();
            CreateMap<UserDto, UserLoggedDto>().ReverseMap();
        }
    }
}
