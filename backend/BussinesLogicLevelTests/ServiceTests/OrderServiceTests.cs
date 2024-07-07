﻿using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderService _orderService;
        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IRepository<Order>>();
            _mockMapper = new Mock<IMapper>();
            _orderService = new OrderService(_mockOrderRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnAddedOrderDto()
        {
            // Arrange
            var orderDto = new OrderDto { UserId = Guid.NewGuid(), OrderDate = DateTime.UtcNow };
            var order = new Order { CartId = orderDto.UserId, OrderDate = orderDto.OrderDate };
            var addedOrder = new Order { Id = Guid.NewGuid(), CartId = orderDto.UserId, OrderDate = orderDto.OrderDate };

            _mockMapper.Setup(m => m.Map<Order>(orderDto)).Returns(order);
            _mockOrderRepository.Setup(r => r.AddAsync(order)).ReturnsAsync(addedOrder);
            _mockMapper.Setup(m => m.Map<OrderDto>(addedOrder)).Returns(orderDto);

            // Act
            var result = await _orderService.AddAsync(orderDto);

            // Assert
            result.Should().BeEquivalentTo(orderDto);
            _mockOrderRepository.Verify(r => r.AddAsync(order), Times.Once);
        }
        
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrderDto()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId, CartId = Guid.NewGuid(), OrderDate = DateTime.UtcNow };
            var orderDto = new OrderDto { UserId = order.CartId, OrderDate = order.OrderDate };

            _mockOrderRepository.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockMapper.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);

            // Act
            var result = await _orderService.GetByIdAsync(orderId);

            // Assert
            result.Should().BeEquivalentTo(orderDto);
            _mockOrderRepository.Verify(r => r.GetByIdAsync(orderId), Times.Once);
        }
        [Fact]
        public async Task RemoveAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId, CartId = Guid.NewGuid(), OrderDate = DateTime.UtcNow };

            _mockOrderRepository.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockOrderRepository.Setup(r => r.DeleteAsync(order)).Returns(Task.CompletedTask);

            // Act
            await _orderService.RemoveAsync(orderId);

            // Assert
            _mockOrderRepository.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            _mockOrderRepository.Verify(r => r.DeleteAsync(order), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var orderDto = new OrderDto { CartId = Guid.NewGuid(), OrderDate = DateTime.UtcNow };
            var order = new Order { Id = Guid.NewGuid(), CartId = orderDto.CartId, OrderDate = orderDto.OrderDate };

            _mockMapper.Setup(m => m.Map<Order>(orderDto)).Returns(order);
            _mockOrderRepository.Setup(r => r.UpdateAsync(order)).ReturnsAsync(order);

            // Act
            await _orderService.UpdateAsync(orderDto);

            // Assert
            _mockOrderRepository.Verify(r => r.UpdateAsync(order), Times.Once);
        }
    }
}
