using AutoMapper;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace Infrastucture.MappingProfilies
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
        }
    }
}
