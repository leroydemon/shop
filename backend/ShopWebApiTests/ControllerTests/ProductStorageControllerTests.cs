using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class ProductStorageControllerTests
    {
        private readonly Mock<IProductStorageService> _mockProductStorageService;
        private readonly Mock<ILogger<ProductStorageController>> _mockLogger;
        private readonly ProductStorageController _productStorageController;
        public ProductStorageControllerTests()
        {
            _mockProductStorageService = new Mock<IProductStorageService>();
            _mockLogger = new Mock<ILogger<ProductStorageController>>();
            _productStorageController = new ProductStorageController(_mockProductStorageService.Object, _mockLogger.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnOkWithProductStorages()
        {
            // Arrange
            var productStorages = new List<ProductStorageDto>
            {
                new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 10 },
                new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 20 }
            };
            _mockProductStorageService.Setup(service => service.GetAllAsync()).ReturnsAsync(productStorages);

            // Act
            var result = await _productStorageController.GetAllAsync();

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("method works correctly")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(productStorages);
        }
        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnOkWithProductStorage()
        {
            // Arrange
            var storageId = Guid.NewGuid();
            var productStorage = new ProductStorageDto { StorageId = storageId, ProductId = Guid.NewGuid(), Quantity = 10 };
            _mockProductStorageService.Setup(service => service.GetByIdAsync(storageId)).ReturnsAsync(productStorage);

            // Act
            var result = await _productStorageController.GetProductByIdAsync(storageId);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("method works correctly")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(productStorage);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnOkWithProductStorage()
        {
            // Arrange
            var productDto = new ProductStorageDto { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 10 };
            var addedProductStorage = new ProductStorageDto { StorageId = productDto.StorageId, ProductId = productDto.ProductId, Quantity = productDto.Quantity };
            _mockProductStorageService.Setup(service => service.AddAsync(productDto)).ReturnsAsync(addedProductStorage);

            // Act
            var result = await _productStorageController.AddAsync(productDto);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("method works correctly")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(addedProductStorage);
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var productDto = new ProductStorage { StorageId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 20 };

            _mockProductStorageService.Setup(service => service.UpdateAsync(productDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _productStorageController.UpdateAsync(productDto);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("method works correctly")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockProductStorageService.Verify(service => service.UpdateAsync(productDto), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mockProductStorageService.Setup(service => service.DeleteAsync(productId)).Returns(Task.CompletedTask);

            // Act
            var result = await _productStorageController.DeleteAsync(productId);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("method works correctly")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockProductStorageService.Verify(service => service.DeleteAsync(productId), Times.Once);
        }
    }
}
