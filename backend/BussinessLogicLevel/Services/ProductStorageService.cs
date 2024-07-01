using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class ProductStorageService : IProductStorageService
    {
        private readonly Repository<ProductStorage> _prRepository;
        private readonly IMapper _mapper;
        public ProductStorageService(Repository<ProductStorage> prRepository, IMapper mapper)
        {
            _prRepository = prRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductStorageDto>> GetAllAsync()
        {
            var items =  await _prRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductStorageDto>>(items);
        }
        public async Task<ProductStorageDto> GetByIdAsync(Guid productStorageId)
        {
            var productStorage = await _prRepository.GetByIdAsync(productStorageId);
            return _mapper.Map<ProductStorageDto>(productStorage);
        }
        public async Task DeleteAsync(Guid id)
        {
            var productStorage = await _prRepository.GetByIdAsync(id);
            await _prRepository.DeleteAsync(productStorage);
        }
        public async Task UpdateAsync(ProductStorageDto productStorageDto)
        {
            var productStorage = _mapper.Map<ProductStorage>(productStorageDto);
            await _prRepository.UpdateAsync(productStorage);
        }
        public async Task<ProductStorageDto> AddAsync(ProductStorageDto productStorageDto)
        {
            var productStorage = _mapper.Map<ProductStorage>(productStorageDto);
            var addedProductStorage =  await _prRepository.AddAsync(productStorage);
            return _mapper.Map<ProductStorageDto>(addedProductStorage);
        }
    }
}
