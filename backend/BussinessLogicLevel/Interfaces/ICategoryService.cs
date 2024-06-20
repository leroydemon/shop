using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> AddAsync(CategoryDto category);
        Task<CategoryDto> UpdateAsync(int id, CategoryDto category);
        Task<int> DeleteAsync(int id);
    }
}
