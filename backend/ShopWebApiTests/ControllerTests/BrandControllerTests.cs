using Xunit;
using Moq;
using FluentAssertions;
using BussinessLogicLevel.Interfaces;
using ShopWebApi.Controllers;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApiTests.ControllerTests
{
    public class BrandControllerTests
    {
        private readonly Mock<IBrandService> _mockBrandService;
        private readonly BrandController _brandController;

        public BrandControllerTests()
        {
            _mockBrandService = new Mock<IBrandService>();
            _brandController = new BrandController(_mockBrandService.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnOkWithBrands()
        {
            // Arrange
            var brands = new List<BrandDto>
            {
                new BrandDto { Name = "Brand1" },
                new BrandDto { Name = "Brand2" }
            };
            _mockBrandService.Setup(service => service.GetAllAsync()).ReturnsAsync(brands);

            // Act
            var result = await _brandController.GetAllAsync();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(brands);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkWithBrand()
        {
            // Arrange
            var brandId = Guid.NewGuid();
            var brand = new BrandDto { Name = "Brand1" };
            _mockBrandService.Setup(service => service.GetByIdAsync(brandId)).ReturnsAsync(brand);

            // Act
            var result = await _brandController.GetByIdAsync(brandId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(brand);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnOk()
        {
            // Arrange
            var brand = new BrandDto { Name = "Brand1" };

            // Act
            var result = await _brandController.AddAsync(brand);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockBrandService.Verify(service => service.AddAsync(brand), Times.Once);
        }
        [Fact]
        public async Task RemoveAsync_ShouldReturnOk()
        {
            // Arrange
            var brandId = Guid.NewGuid();

            // Act
            var result = await _brandController.RemoveAsync(brandId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockBrandService.Verify(service => service.RemoveAsync(brandId), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var brand = new Brand { Name = "UpdatedBrand" };

            // Act
            var result = await _brandController.UpdateAsync(brand);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockBrandService.Verify(service => service.UpdateAsync(brand), Times.Once);
        }
    }
}
