using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<IRepository<Category>> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _categoryService;
        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<IRepository<Category>>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnAddedCategoryDto()
        {
            // Arrange
            var categoryDto = new CategoryDto { Name = "New Category" };
            var category = new Category { Name = "New Category" };
            var addedCategory = new Category { Id = Guid.NewGuid(), Name = "New Category" };

            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(category);
            _mockCategoryRepository.Setup(r => r.AddAsync(category)).ReturnsAsync(addedCategory);
            _mockMapper.Setup(m => m.Map<CategoryDto>(addedCategory)).Returns(categoryDto);

            // Act
            var result = await _categoryService.AddAsync(categoryDto);

            // Assert
            result.Should().BeEquivalentTo(categoryDto);
            _mockCategoryRepository.Verify(r => r.AddAsync(category), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "Test Category" };

            _mockCategoryRepository.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _mockCategoryRepository.Setup(r => r.DeleteAsync(category)).Returns(Task.CompletedTask);

            // Act
            await _categoryService.DeleteAsync(categoryId);

            // Assert
            _mockCategoryRepository.Verify(r => r.GetByIdAsync(categoryId), Times.Once);
            _mockCategoryRepository.Verify(r => r.DeleteAsync(category), Times.Once);
        }
       
        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategoryDto()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "Test Category" };
            var categoryDto = new CategoryDto { Name = "Test Category" };

            _mockCategoryRepository.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<CategoryDto>(category)).Returns(categoryDto);

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            result.Should().BeEquivalentTo(categoryDto);
            _mockCategoryRepository.Verify(r => r.GetByIdAsync(categoryId), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var categoryDto = new CategoryDto { Id = Guid.NewGuid(), Name = "Updated Category" };
            var category = new Category { Id = categoryDto.Id, Name = "Original Category" };

            _mockCategoryRepository.Setup(r => r.GetByIdAsync(categoryDto.Id)).ReturnsAsync(category);
            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(category);
            _mockCategoryRepository.Setup(r => r.UpdateAsync(It.IsAny<Category>())).ReturnsAsync(category);

            // Act
            await _categoryService.UpdateAsync(categoryDto);

            // Assert
            _mockCategoryRepository.Verify(r => r.UpdateAsync(category), Times.Once);
        }
    }
}

