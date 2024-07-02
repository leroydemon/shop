using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class StorageServiceTests
    {
        private readonly Mock<IRepository<Storage>> _mockStorageRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StorageService _storageService;

        public StorageServiceTests()
        {
            _mockStorageRepository = new Mock<IRepository<Storage>>();
            _mockMapper = new Mock<IMapper>();
            _storageService = new StorageService(_mockStorageRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnStorageDto()
        {
            // Arrange
            var storageId = Guid.NewGuid();
            var storage = new Storage { Id = storageId, City = "City1", Address = "Address1", Phone = "123456789" };
            var storageDto = new StorageDto { City = "City1", Address = "Address1", Phone = "123456789" };

            _mockStorageRepository.Setup(r => r.GetByIdAsync(storageId)).ReturnsAsync(storage);
            _mockMapper.Setup(m => m.Map<StorageDto>(storage)).Returns(storageDto);

            // Act
            var result = await _storageService.GetByIdAsync(storageId);

            // Assert
            result.Should().BeEquivalentTo(storageDto);
            _mockStorageRepository.Verify(r => r.GetByIdAsync(storageId), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnStorageDtos()
        {
            // Arrange
            var storages = new List<Storage>
            {
                new Storage { Id = Guid.NewGuid(), City = "City1", Address = "Address1", Phone = "123456789" },
                new Storage { Id = Guid.NewGuid(), City = "City2", Address = "Address2", Phone = "987654321" }
            };
            var storageDtos = new List<StorageDto>
            {
                new StorageDto { City = "City1", Address = "Address1", Phone = "123456789" },
                new StorageDto { City = "City2", Address = "Address2", Phone = "987654321" }
            };

            _mockStorageRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(storages);
            _mockMapper.Setup(m => m.Map<IEnumerable<StorageDto>>(storages)).Returns(storageDtos);

            // Act
            var result = await _storageService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(storageDtos);
            _mockStorageRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnAddedStorageDto()
        {
            // Arrange
            var storageDto = new StorageDto { City = "City1", Address = "Address1", Phone = "123456789" };
            var storage = new Storage { City = "City1", Address = "Address1", Phone = "123456789" };
            var addedStorage = new Storage { Id = Guid.NewGuid(), City = "City1", Address = "Address1", Phone = "123456789" };

            _mockMapper.Setup(m => m.Map<Storage>(storageDto)).Returns(storage);
            _mockStorageRepository.Setup(r => r.AddAsync(storage)).ReturnsAsync(addedStorage);
            _mockMapper.Setup(m => m.Map<StorageDto>(addedStorage)).Returns(storageDto);

            // Act
            var result = await _storageService.AddAsync(storageDto);

            // Assert
            result.Should().BeEquivalentTo(storageDto);
            _mockStorageRepository.Verify(r => r.AddAsync(storage), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var storageId = Guid.NewGuid();
            var storage = new Storage { Id = storageId, City = "City1", Address = "Address1", Phone = "123456789" };

            _mockStorageRepository.Setup(r => r.GetByIdAsync(storageId)).ReturnsAsync(storage);
            _mockStorageRepository.Setup(r => r.DeleteAsync(storage)).Returns(Task.CompletedTask);

            // Act
            await _storageService.DeleteAsync(storageId);

            // Assert
            _mockStorageRepository.Verify(r => r.GetByIdAsync(storageId), Times.Once);
            _mockStorageRepository.Verify(r => r.DeleteAsync(storage), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var storageDto = new StorageDto { City = "Updated City", Address = "Updated Address", Phone = "987654321" };
            var storage = new Storage { Id = Guid.NewGuid(), City = "Updated City", Address = "Updated Address", Phone = "987654321" };

            _mockMapper.Setup(m => m.Map<Storage>(storageDto)).Returns(storage);
            _mockStorageRepository.Setup(r => r.UpdateAsync(storage)).ReturnsAsync(storage);

            // Act
            await _storageService.UpdateAsync(storageDto);

            // Assert
            _mockStorageRepository.Verify(r => r.UpdateAsync(storage), Times.Once);
        }
    }
}
