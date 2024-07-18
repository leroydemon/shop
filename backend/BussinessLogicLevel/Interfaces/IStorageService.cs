
using DbLevel.Filters;
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IStorageService
    {
        Task<IEnumerable<StorageDto>> SearchAsync(StorageFilter filter);
        Task<StorageDto> AddAsync(StorageDto storageDto);
        Task<StorageDto> UpdateAsync(StorageDto storage);
        Task DeleteAsync(Guid id);
        public Task<StorageDto> GetByIdAsync(Guid id);
    }
}
