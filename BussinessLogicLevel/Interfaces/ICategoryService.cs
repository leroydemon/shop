
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetCategoryById(int id);
        void Add(Category category);
        void Update(int id, Category category);
        void Delete(int id);
    }
}
