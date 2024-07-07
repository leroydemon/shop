using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;

namespace BussinessLogicLevel.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {

            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            var addedCategory = await _categoryRepository.AddAsync(_mapper.Map<Category>(categoryDto));

            return _mapper.Map<CategoryDto>(addedCategory);
        }
        public async Task DeleteAsync(Guid id)
        {
            var item = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(item);
        }
        public async Task<IEnumerable<CategoryDto>> SearchAsync(CategoryFilter filter)
        {
            var spec = new CategorySpecification(filter);
            var categories = await _categoryRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> UpdateAsync(CategoryDto category)
        {
            var updatedCategory = await _categoryRepository.UpdateAsync(_mapper.Map<Category>(category));

            return _mapper.Map<CategoryDto>(updatedCategory);
        }
    }
}
