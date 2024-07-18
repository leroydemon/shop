using DbLevel.Filters;
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> SearchAsync(BrandFilter filter);
        Task<BrandDto> AddAsync(BrandDto brand);
        Task<BrandDto> UpdateAsync(BrandDto brand);
        Task RemoveAsync(Guid id);
        Task<BrandDto> GetByIdAsync(Guid id);
    }
}
