using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDto> AddAsync(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            var addedBrand = await _brandRepository.AddAsync(brand);
            return _mapper.Map<BrandDto>(addedBrand);
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brand = await _brandRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brand);
        }

        public async Task<BrandDto> GetByIdAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task RemoveAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            await _brandRepository.DeleteAsync(brand);
        }

        public async Task UpdateAsync(Brand brand)
        {
            var brandMapped = _mapper.Map<Brand>(brand);
            await _brandRepository.UpdateAsync(brandMapped);
        }
    }
}
