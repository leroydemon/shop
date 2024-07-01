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
    public class StorageControllerTests
    {
        private readonly Mock<IStorageService> _mockStorageService;
        private readonly Mock<ILogger<StorageController>> _mockLogger;
        private readonly StorageController _storageController;

        public StorageControllerTests()
        {
            _mockStorageService = new Mock<IStorageService>();
            _mockLogger = new Mock<ILogger<StorageController>>();
            _storageController = new StorageController(_mockStorageService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkWithStorages()
        {
            // Arrange
            var storages = new List<StorageDto>
            {
                new StorageDto { City = "City1", Address = "Address1", Phone = "1234567890" },
                new StorageDto { City = "City2", Address = "Address2", Phone = "0987654321" }
            };
            _mockStorageService.Setup(service => service.GetAllAsync()).ReturnsAsync(storages);

            // Act
            var result = await _storageController.GetAllAsync();

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
            okResult.Value.Should().BeEquivalentTo(storages);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnOkWithStorage()
        {
            // Arrange
            var storageId = Guid.NewGuid();
            var storage = new StorageDto { City = "City1", Address = "Address1", Phone = "1234567890" };
            _mockStorageService.Setup(service => service.GetByIdAsync(storageId)).ReturnsAsync(storage);

            // Act
            var result = await _storageController.GetProductByIdAsync(storageId);

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
            okResult.Value.Should().BeEquivalentTo(storage);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnOkWithStorage()
        {
            // Arrange
            var storageDto = new StorageDto { City = "City1", Address = "Address1", Phone = "1234567890" };
            var addedStorage = new StorageDto { City = "City1", Address = "Address1", Phone = "1234567890" };
            _mockStorageService.Setup(service => service.AddAsync(storageDto)).ReturnsAsync(addedStorage);

            // Act
            var result = await _storageController.AddAsync(storageDto);

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
            okResult.Value.Should().BeEquivalentTo(addedStorage);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var storageDto = new Storage { City = "City1", Address = "Address1", Phone = "1234567890" };

            _mockStorageService.Setup(service => service.UpdateAsync(storageDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _storageController.UpdateAsync(storageDto);

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
            _mockStorageService.Verify(service => service.UpdateAsync(storageDto), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange
            var storageId = Guid.NewGuid();

            _mockStorageService.Setup(service => service.DeleteAsync(storageId)).Returns(Task.CompletedTask);

            // Act
            var result = await _storageController.DeleteAsync(storageId);

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
            _mockStorageService.Verify(service => service.DeleteAsync(storageId), Times.Once);
        }
    }
}
