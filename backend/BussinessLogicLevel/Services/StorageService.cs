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
        public async Task UpdateAsync(Storage storage)
        {
            var storageMapped = _mapper.Map<Storage>(storage);
            await _storageRepository.UpdateAsync(storageMapped);
        }
        public async Task<StorageDto> AddAsync(StorageDto storageDto)
        {
            var storage = _mapper.Map<Storage>(storageDto);
            var addedStorage = await _storageRepository.AddAsync(storage);
            return _mapper.Map<StorageDto>(addedStorage);
        }
    }
}
