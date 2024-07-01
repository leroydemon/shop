﻿using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<ILogger<CategoryController>> _mockLogger;
        private readonly CategoryController _categoryController;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockLogger = new Mock<ILogger<CategoryController>>();
            _categoryController = new CategoryController(_mockCategoryService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkWithCategories()
        {
            // Arrange
            var categories = new List<CategoryDto>
            {
                new CategoryDto { Name = "Category1" },
                new CategoryDto { Name = "Category2" }
            };
            _mockCategoryService.Setup(service => service.GetAllAsync()).ReturnsAsync((IEnumerable<CategoryDto>)categories);

            // Act
            var result = await _categoryController.GetAllAsync();

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Я работаю!")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnOkWithCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new CategoryDto { Name = "Category1" };
            _mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _categoryController.GetProductByIdAsync(categoryId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnOk()
        {
            // Arrange
            var category = new CategoryDto { Name = "Category1" };

            // Act
            var result = await _categoryController.AddAsync(category);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockCategoryService.Verify(service => service.AddAsync(category), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var category = new Category { Id = Guid.NewGuid(), Name = "UpdatedCategory" };

            // Act
            var result = await _categoryController.UpdateAsync(category);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockCategoryService.Verify(service => service.UpdateAsync(category), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Act
            var result = await _categoryController.DeleteAsync(categoryId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockCategoryService.Verify(service => service.DeleteAsync(categoryId), Times.Once);
        }
    }
}
