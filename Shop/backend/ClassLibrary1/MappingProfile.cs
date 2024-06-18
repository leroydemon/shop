using AutoMapper;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace Infrastucture
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //separeate file per model
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}
