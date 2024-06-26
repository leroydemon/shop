﻿using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> AddAsync(CategoryDto category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}
