using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class CartControllerTests
    {
        private readonly Mock<ICartService> _mockCartService;
        private readonly CartController _cartController;
        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartService>();
            _cartController = new CartController(_mockCartService.Object);
        }
        [Fact]
        public async Task AddToAsync_ShouldReturnOkWithCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 2;
            var cart = new CartDto();

            _mockCartService.Setup(service => service.AddToAsync(cartId, productId, quantity)).ReturnsAsync(cart);

            // Act
            var result = await _cartController.AddToAsync(cartId, productId, quantity);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(cart);
        }
        [Fact]
        public async Task ClearAsync_ShouldReturnOk()
        {
            // Arrange
            var cartId = Guid.NewGuid();

            _mockCartService.Setup(service => service.ClearAsync(cartId)).Returns(Task.CompletedTask);

            // Act
            var result = await _cartController.ClearAsync(cartId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockCartService.Verify(service => service.ClearAsync(cartId), Times.Once);
        }
        [Fact]
        public async Task GetAsync_ShouldReturnOkWithCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new CartDto();

            _mockCartService.Setup(service => service.GetAsync(cartId)).ReturnsAsync(cart);

            // Act
            var result = await _cartController.GetAsync(cartId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(cart);
        }
        [Fact]
        public async Task RemoveFromAsync_ShouldReturnOk()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 1;

            _mockCartService.Setup(service => service.RemoveFromAsync(cartId, productId, quantity)).Returns(Task.CompletedTask);

            // Act
            var result = await _cartController.RemoveFromAsync(cartId, productId, quantity);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockCartService.Verify(service => service.RemoveFromAsync(cartId, productId, quantity), Times.Once);
        }
    }
}