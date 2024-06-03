
using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetProductById(int id);
        Task Add(Category category);
        Task Delete(Category category);
        Task Update(int id, Category category);
    }
}
