using DbLevel.Models;
namespace DbLevel.Interface
{
    public interface IPromoCodeRepository
    {
        Task<PromoCode> GetAsync(string code);
        Task<List<PromoCode>> GetAllAsync();
        Task CreateAsync(PromoCode promoCode);
        Task UpdateAsync(PromoCode promoCode);
        Task DeleteAsync(PromoCode code);
        public Task<PromoCode> FindByIdAsync(string id);
    }
}

