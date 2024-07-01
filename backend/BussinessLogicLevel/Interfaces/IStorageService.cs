
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IStorageService
    {
        Task<IEnumerable<StorageDto>> GetAllAsync();
        Task<StorageDto> AddAsync(StorageDto storageDto);
        Task UpdateAsync(Storage storage);
        Task DeleteAsync(Guid id);
        public Task<StorageDto> GetByIdAsync(Guid id);
    }
}
