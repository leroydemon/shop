using AutoMapper;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace Infrastucture.MappingProfilies
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>()
                .ReverseMap();
        }
    }
}
