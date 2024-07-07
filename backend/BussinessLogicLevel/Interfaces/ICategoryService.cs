using DbLevel.Filters;
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> SearchAsync(CategoryFilter filter);
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> AddAsync(CategoryDto category);
        Task<CategoryDto> UpdateAsync(CategoryDto category);
        Task DeleteAsync(Guid id);
    }
}
