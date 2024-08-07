﻿using BussinessLogicLevel.Interfaces;
using DbLevel.SortableFields;
using FluentAssertions;
using Infrastucture.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;
        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }
        [Fact]
        public async Task SetOfflineAsync_ShouldReturnOk()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(service => service.SetOfflineAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.SetOfflineAsync(userId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockUserService.Verify(service => service.SetOfflineAsync(userId), Times.Once);
        }
        [Fact]
        public async Task SetOnlineAsync_ShouldReturnOk()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(service => service.SetOnlineAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.SetOnlineAsync(userId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockUserService.Verify(service => service.SetOnlineAsync(userId), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(service => service.RemoveAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.DeleteAsync(userId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockUserService.Verify(service => service.RemoveAsync(userId), Times.Once);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkWithUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new UserDto { UserName = "User1", Surname = "Surname1" };

            _mockUserService.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userController.GetByIdAsync(userId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(user);
        }
    }
}
