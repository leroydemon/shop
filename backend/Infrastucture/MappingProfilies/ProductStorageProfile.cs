using AutoMapper;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace Infrastucture.MappingProfilies
{
    public class ProductStorageProfile : Profile
    {
        public ProductStorageProfile()
        {
            CreateMap<ProductStorage, ProductStorageDto>()
                .ReverseMap();
        }
    }
}
