using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(int id, Category category);
        Task DeleteAsync(int id);
    }
}
