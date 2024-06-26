﻿using DbLevel.Models;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> AddAsync(ProductDto product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
