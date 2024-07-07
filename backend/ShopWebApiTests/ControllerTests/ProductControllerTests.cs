using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Infrastucture.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ILogger<ProductController>> _mockLogger;
        private readonly ProductController _productController;
        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _productController = new ProductController(_mockProductService.Object, _mockLogger.Object);
        }
        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnOkWithProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductDto { Name = "Product1" };
            _mockProductService.Setup(service => service.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productController.GetProductByIdAsync(productId);

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
            okResult.Value.Should().BeEquivalentTo(product);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnOkWithProduct()
        {
            // Arrange
            var productDto = new ProductDto { Name = "Product1" };
            var addedProduct = new ProductDto { Name = "Product1" };
            _mockProductService.Setup(service => service.AddAsync(productDto)).ReturnsAsync(addedProduct);

            // Act
            var result = await _productController.AddAsync(productDto);

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
            okResult.Value.Should().BeEquivalentTo(addedProduct);
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var productDto = new ProductDto { Name = "UpdatedProduct" };

            _mockProductService.Setup(service => service.UpdateAsync(productDto)).ReturnsAsync(productDto);

            // Act
            var result = await _productController.UpdateAsync(productDto);

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
            _mockProductService.Verify(service => service.UpdateAsync(productDto), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mockProductService.Setup(service => service.DeleteAsync(productId)).Returns(Task.CompletedTask);

            // Act
            var result = await _productController.DeleteAsync(productId);

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
            _mockProductService.Verify(service => service.DeleteAsync(productId), Times.Once);
        }
    }
}
