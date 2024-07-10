using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRepository<Storage> _storageRepository;
        private readonly IMapper _mapper;
        public StorageService(IRepository<Storage> storage, IMapper mapper)
        {
            _storageRepository = storage;
            _mapper = mapper;
        }
        public async Task<StorageDto> GetByIdAsync(Guid id)
        {
            var storage = await _storageRepository.GetByIdAsync(id);
            return _mapper.Map<StorageDto>(storage);
        }
        public async Task<IEnumerable<StorageDto>> GetAllAsync()
        {
            var items = await _storageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StorageDto>>(items);
        }
        public async Task DeleteAsync(Guid id)
        {
            var storage = await _storageRepository.GetByIdAsync(id);
            await _storageRepository.DeleteAsync(storage);
        }
        public async Task<StorageDto> UpdateAsync(StorageDto storage)
        {
            var updatedStorage = await _storageRepository.UpdateAsync(_mapper.Map<Storage>(storage));
            return _mapper.Map<StorageDto>(updatedStorage);
        }
        public async Task<StorageDto> AddAsync(StorageDto storageDto)
        {
            var addedStorage = await _storageRepository.AddAsync(_mapper.Map<Storage>(storageDto));
            return _mapper.Map<StorageDto>(addedStorage);
        }
    }
}
