using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;

namespace BussinessLogicLevel.Services
{
    public class ProductStorageService : IProductStorageService
    {
        private readonly IRepository<ProductStorage> _prRepository;
        private readonly IMapper _mapper;
        public ProductStorageService(IRepository<ProductStorage> prRepository, IMapper mapper)
        {
            _prRepository = prRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductStorageDto>> SearchAsync(ProductStorageFilter filter)
        {
            var spec = new ProductStorageSpecification(filter);
            var items =  await _prRepository.ListAsync(spec);

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
        public async Task<ProductStorageDto> UpdateAsync(ProductStorageDto productStorage)
        {
            var updatedProductStorage = await _prRepository.UpdateAsync(_mapper.Map<ProductStorage>(productStorage));

            return _mapper.Map<ProductStorageDto>(updatedProductStorage);
        }
        public async Task<ProductStorageDto> AddAsync(ProductStorageDto productStorageDto)
        {
            var addedProductStorage =  await _prRepository.AddAsync(_mapper.Map<ProductStorage>(productStorageDto));

            return _mapper.Map<ProductStorageDto>(addedProductStorage);
        }
    }
}
