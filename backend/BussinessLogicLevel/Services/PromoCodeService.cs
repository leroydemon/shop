using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;

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
        public async Task<PromoCodeDto> AddAsync(PromoCodeDto promoCodeDto)
        {
            var addedPromoCode = await _promoCodeRepository.AddAsync(_mapper.Map<PromoCode>(promoCodeDto));

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
        public async Task<IEnumerable<PromoCodeDto>> SearchAsync(PromoCodeFilter filter)
        {
            var spec = new PromoCodeSpecification(filter);
            var items =  await _promoCodeRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<PromoCodeDto>>(items);
        }
        public async Task<PromoCodeDto> UpdateAsync(PromoCodeDto promoCode)
        {
            var updatedPromoCode = await _promoCodeRepository.UpdateAsync(_mapper.Map<PromoCode>(promoCode));

            return _mapper.Map<PromoCodeDto>(updatedPromoCode);
        }
    }
}
