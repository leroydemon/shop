using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _orderController = new OrderController(_mockOrderService.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkWithOrders()
        {
            // Arrange
            var orders = new List<OrderDto>
            {
                new OrderDto(),
                new OrderDto()
            };
            _mockOrderService.Setup(service => service.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderController.GetAllAsync();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(orders);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkWithOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new OrderDto();
            _mockOrderService.Setup(service => service.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderController.GetByIdAsync(orderId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCreated()
        {
            // Arrange
            var orderDto = new OrderDto();

            // Act
            var result = await _orderController.AddAsync(orderDto);

            // Assert
            var createdResult = result as OkObjectResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(200);
            _mockOrderService.Verify(service => service.AddAsync(orderDto), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnOk()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Act
            var result = await _orderController.RemoveAsync(orderId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockOrderService.Verify(service => service.RemoveAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var orderDto = new Order();

            // Act
            var result = await _orderController.UpdateAsync(orderDto);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockOrderService.Verify(service => service.UpdateAsync(orderDto), Times.Once);
        }
    }
}
