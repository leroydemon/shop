using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly Repository<PromoCode> _promoCodeRepository;
        private readonly IMapper _mapper;
        public PromoCodeService(Repository<PromoCode> promoCodeRepository)
        {
            _promoCodeRepository = promoCodeRepository;
        }
        public async Task<PromoCodeDto> CreateAsync(PromoCodeDto promoCodeDto)
        {
            var promoCode = _mapper.Map<PromoCode>(promoCodeDto);
            var addedPromoCode = await _promoCodeRepository.AddAsync(promoCode);
            return _mapper.Map<PromoCodeDto>(addedPromoCode);
        }
        public async Task DeleteAsync(Guid id) 
        {
            var promoCode = await _promoCodeRepository.GetByIdAsync(id);
            await _promoCodeRepository.DeleteAsync(promoCode);
        }
        public async Task<PromoCodeDto> FindByIdAsync(Guid id)
        {
            var promoCode =  await _promoCodeRepository.GetByIdAsync(id);
            return _mapper.Map<PromoCodeDto>(promoCode);
        }
        public async Task<IEnumerable<PromoCodeDto>> GetAllAsync()
        {
            var items =  await _promoCodeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PromoCodeDto>>(items);
        }
        public async Task UpdateAsync(PromoCodeDto promoCodeDto)
        {
            var promoCode = _mapper.Map<PromoCode>(promoCodeDto);
            await _promoCodeRepository.UpdateAsync(promoCode);
        }
    }
}
