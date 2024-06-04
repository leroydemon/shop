using BussinessLogicLevel.Interfaces;
using DbLevel.Interface;
using DbLevel.Models;
using DbLevel.Repository;

namespace BussinessLogicLevel.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {

            _categoryRepository = categoryRepository;

        }
        public async Task AddAsync(Category product)
        {
           await _categoryRepository.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
             var item = await _categoryRepository.GetProductByIdAsync(id);
            await _categoryRepository.DeleteAsync(item);
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }


        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetProductByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Category product)
        {
            await _categoryRepository.UpdateAsync(id, product);
        }
    }
}
