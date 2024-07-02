using AutoMapper;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace Infrastucture
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>()
                .ReverseMap();
            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<ProductStorage, ProductStorageDto>()
                .ReverseMap();
            CreateMap<Storage, StorageDto>()
                .ReverseMap();
            CreateMap<Brand, BrandDto>()
                .ReverseMap();
            CreateMap<Cart, CartDto>()
                .ReverseMap();
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            CreateMap<Order, OrderDto>()
                .ReverseMap();
            CreateMap<PromoCode, PromoCodeDto>()
                .ReverseMap();
        }
    }
}
