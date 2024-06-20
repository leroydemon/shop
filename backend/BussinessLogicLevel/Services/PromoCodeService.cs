using BussinessLogicLevel.Interfaces;
using DbLevel.Interface;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        public PromoCodeService(IPromoCodeRepository promoCodeRepository)
        {
            _promoCodeRepository = promoCodeRepository;
        }
        public async Task CreateAsync(PromoCode promoCode)
        {
            await _promoCodeRepository.CreateAsync(promoCode);
        }

        public async Task DeleteAsync(string id)
        {
            var promoCode = await _promoCodeRepository.FindByIdAsync(id);
            await _promoCodeRepository.DeleteAsync(promoCode);
        }

        public async Task<PromoCode> FindByIdAsync(string id)
        {
            return await _promoCodeRepository.FindByIdAsync(id);
        }

        public async Task<List<PromoCode>> GetAllAsync()
        {
            return await _promoCodeRepository.GetAllAsync();
        }

        public async Task<PromoCode> GetAsync(string code)
        {
            return await _promoCodeRepository.GetAsync(code);
        }

        public async Task UpdateAsync(string id)
        {
             var promoCode = await _promoCodeRepository.FindByIdAsync(id);
             await _promoCodeRepository.UpdateAsync(promoCode);
        }
    }
}
