
using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetProductByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<int> DeleteAsync(int id);
        Task<Category> UpdateAsync(Category category);
    }
}
