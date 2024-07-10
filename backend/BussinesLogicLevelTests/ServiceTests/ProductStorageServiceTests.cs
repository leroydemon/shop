using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class ProductStorageServiceTests
    {
        private readonly Mock<IRepository<ProductStorage>> _mockProductStorageRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductStorageService _productStorageService;
        public ProductStorageServiceTests()
        {
            _mockProductStorageRepository = new Mock<IRepository<ProductStorage>>();
            _mockMapper = new Mock<IMapper>();
            _productStorageService = new ProductStorageService(_mockProductStorageRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnProductStorageDtos()
        {
            // Arrange
            var productStorages = new List<ProductStorage>
            {
                new ProductStorage { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 50 },
                new ProductStorage { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 100 }
            };
            var productStorageDtos = new List<ProductStorageDto>
            {
                new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 50 },
                new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 100 }
            };

            _mockProductStorageRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(productStorages);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductStorageDto>>(productStorages)).Returns(productStorageDtos);

            // Act
            var result = await _productStorageService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(productStorageDtos);
            _mockProductStorageRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProductStorageDto()
        {
            // Arrange
            var productStorageId = Guid.NewGuid();
            var productStorage = new ProductStorage { StorageId = productStorageId, ProductId = Guid.NewGuid(), Quantity = 50 };
            var productStorageDto = new ProductStorageDto { StorageId = productStorageId, ProductId = Guid.NewGuid(), Quantity = 50 };

            _mockProductStorageRepository.Setup(r => r.GetByIdAsync(productStorageId)).ReturnsAsync(productStorage);
            _mockMapper.Setup(m => m.Map<ProductStorageDto>(productStorage)).Returns(productStorageDto);

            // Act
            var result = await _productStorageService.GetByIdAsync(productStorageId);

            // Assert
            result.Should().BeEquivalentTo(productStorageDto);
            _mockProductStorageRepository.Verify(r => r.GetByIdAsync(productStorageId), Times.Once);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnAddedProductStorageDto()
        {
            // Arrange
            var productStorageDto = new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 50 };
            var productStorage = new ProductStorage { StorageId = productStorageDto.StorageId, ProductId = productStorageDto.ProductId, Quantity = productStorageDto.Quantity };
            var addedProductStorage = new ProductStorage { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 50 };

            _mockMapper.Setup(m => m.Map<ProductStorage>(productStorageDto)).Returns(productStorage);
            _mockProductStorageRepository.Setup(r => r.AddAsync(productStorage)).ReturnsAsync(addedProductStorage);
            _mockMapper.Setup(m => m.Map<ProductStorageDto>(addedProductStorage)).Returns(productStorageDto);

            // Act
            var result = await _productStorageService.AddAsync(productStorageDto);

            // Assert
            result.Should().BeEquivalentTo(productStorageDto);
            _mockProductStorageRepository.Verify(r => r.AddAsync(productStorage), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var productStorageId = Guid.NewGuid();
            var productStorage = new ProductStorage { StorageId = productStorageId, ProductId = Guid.NewGuid(), Quantity = 50 };

            _mockProductStorageRepository.Setup(r => r.GetByIdAsync(productStorageId)).ReturnsAsync(productStorage);
            _mockProductStorageRepository.Setup(r => r.DeleteAsync(productStorage)).Returns(Task.CompletedTask);

            // Act
            await _productStorageService.DeleteAsync(productStorageId);

            // Assert
            _mockProductStorageRepository.Verify(r => r.GetByIdAsync(productStorageId), Times.Once);
            _mockProductStorageRepository.Verify(r => r.DeleteAsync(productStorage), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var productStorageDto = new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 50 };
            var productStorage = new ProductStorage { StorageId = productStorageDto.StorageId, ProductId = productStorageDto.ProductId, Quantity = productStorageDto.Quantity };

            _mockMapper.Setup(m => m.Map<ProductStorage>(productStorageDto)).Returns(productStorage);
            _mockProductStorageRepository.Setup(r => r.UpdateAsync(productStorage)).ReturnsAsync(productStorage);

            // Act
            await _productStorageService.UpdateAsync(productStorageDto);

            // Assert
            _mockProductStorageRepository.Verify(r => r.UpdateAsync(productStorage), Times.Once);
        }
    }
}
