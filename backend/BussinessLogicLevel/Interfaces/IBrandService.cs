using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> AddAsync(BrandDto brand);
        Task UpdateAsync(Brand brand);
        Task RemoveAsync(Guid id);
        Task<BrandDto> GetByIdAsync(Guid id);
    }
}
