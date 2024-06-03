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
        public void Add(Category product)
        {
            _categoryRepository.Add(product);
        }

        public void Delete(int id)
        {
             var item = _categoryRepository.GetProductById(id);
            _categoryRepository.Delete(item);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetProductById(id);
        }

        public void Update(int id, Category product)
        {
            _categoryRepository.Update(id, product);
        }
    }
}
