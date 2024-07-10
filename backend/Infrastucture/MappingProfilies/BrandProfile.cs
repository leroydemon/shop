using AutoMapper;
using DbLevel.Models;

namespace Infrastucture.MappingProfilies
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>()
                .ReverseMap();
        }
    }
}
