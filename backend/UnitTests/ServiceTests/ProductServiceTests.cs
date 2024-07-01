using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Infrastucture.DtoModels;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _productService;
        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockMapper = new Mock<IMapper>();
            _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnAddedProductDto()
        {
            // Arrange
            var productDto = new ProductDto { Name = "Test Product", UnitPrice = 100m };
            var product = new Product { Name = "Test Product", UnitPrice = 100m };
            var addedProduct = new Product { Id = Guid.NewGuid(), Name = "Test Product", UnitPrice = 100m };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _mockProductRepository.Setup(r => r.AddAsync(product)).ReturnsAsync(addedProduct);
            _mockMapper.Setup(m => m.Map<ProductDto>(addedProduct)).Returns(productDto);

            // Act
            var result = await _productService.AddAsync(productDto);

            // Assert
            result.Should().BeEquivalentTo(productDto);
            _mockProductRepository.Verify(r => r.AddAsync(product), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product" };

            _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockProductRepository.Setup(r => r.DeleteAsync(product)).Returns(Task.CompletedTask);

            // Act
            await _productService.DeleteAsync(productId);

            // Assert
            _mockProductRepository.Verify(r => r.GetByIdAsync(productId), Times.Once);
            _mockProductRepository.Verify(r => r.DeleteAsync(product), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnProductDtos()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", UnitPrice = 50m },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", UnitPrice = 150m }
            };
            var productDtos = new List<ProductDto>
            {
                new ProductDto { Name = "Product 1", UnitPrice = 50m },
                new ProductDto { Name = "Product 2", UnitPrice = 150m }
            };

            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

            // Act
            var result = await _productService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(productDtos);
            _mockProductRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProductDto()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product", UnitPrice = 100m };
            var productDto = new ProductDto { Name = "Test Product", UnitPrice = 100m };

            _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

            // Act
            var result = await _productService.GetByIdAsync(productId);

            // Assert
            result.Should().BeEquivalentTo(productDto);
            _mockProductRepository.Verify(r => r.GetByIdAsync(productId), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var product = new Product { Id = Guid.NewGuid(), Name = "Updated Product", UnitPrice = 150m };
            var productMapped = new Product { Id = product.Id, Name = "Updated Product", UnitPrice = 150m };

            _mockMapper.Setup(m => m.Map<Product>(product)).Returns(productMapped);
            _mockProductRepository.Setup(r => r.UpdateAsync(productMapped)).Returns(Task.CompletedTask);

            // Act
            await _productService.UpdateAsync(product);

            // Assert
            _mockProductRepository.Verify(r => r.UpdateAsync(product), Times.Once);
        }
    }
}
