using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IRepository<PromoCode> _promoCodeRepository;
        private readonly IMapper _mapper;
        public PromoCodeService(IRepository<PromoCode> promoCodeRepository, IMapper mapper)
        {
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
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
        public async Task UpdateAsync(PromoCode promoCode)
        {
            var promoCodeMapped = _mapper.Map<PromoCode>(promoCode);
            await _promoCodeRepository.UpdateAsync(promoCodeMapped);
        }
    }
}
