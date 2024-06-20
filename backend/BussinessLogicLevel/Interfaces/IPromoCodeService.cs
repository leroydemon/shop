

using DbLevel.Models;
namespace BussinessLogicLevel.Interfaces
{
    public interface IPromoCodeService
    {
        Task<PromoCode> GetAsync(string code);
        Task<List<PromoCode>> GetAllAsync();
        Task CreateAsync(PromoCode promoCode);
        Task UpdateAsync(string id);
        Task DeleteAsync(string id);
        public Task<PromoCode> FindByIdAsync(string id);
    }
}
