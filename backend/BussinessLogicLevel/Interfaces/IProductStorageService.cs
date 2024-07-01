using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IProductStorageService
    {
        public Task<IEnumerable<ProductStorageDto>> GetAllAsync();
        public Task<ProductStorageDto> GetByIdAsync(Guid productStorageId);
        public Task DeleteAsync(Guid id);
        public Task UpdateAsync(ProductStorage productStorage);
        public Task<ProductStorageDto> AddAsync(ProductStorageDto productStorageDto);
    }
}
