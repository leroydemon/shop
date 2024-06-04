
using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetProductByIdAsync(int id);
        Task AddAsync(Category category);
        Task DeleteAsync(Category category);
        Task UpdateAsync(int id, Category category);
    }
}
