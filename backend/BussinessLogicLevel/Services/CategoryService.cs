using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;

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
            var category = _mapper.Map<Category>(categoryDto);
            var addedCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDto>(addedCategory);
        }
        public async Task DeleteAsync(Guid id)
        {
            var item = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(item);
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task UpdateAsync(Category categoryInput)
        {
            var category = _mapper.Map<Category>(categoryInput);
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
