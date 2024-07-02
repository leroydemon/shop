using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IPromoCodeService
    {
        Task<IEnumerable<PromoCodeDto>> GetAllAsync();
        Task<PromoCodeDto> AddAsync(PromoCodeDto promoCode);
        Task<PromoCodeDto> UpdateAsync(PromoCodeDto promoCode);
        Task DeleteAsync(Guid id);
        public Task<PromoCodeDto> FindByIdAsync(Guid id);
    }
}
