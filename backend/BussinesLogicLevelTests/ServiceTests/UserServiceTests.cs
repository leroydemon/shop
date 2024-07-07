using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.SortableFields;
using FluentAssertions;
using Infrastucture.DtoModels;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task RemoveAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };

            _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.DeleteAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.RemoveAsync(userId);

            // Assert
            _mockUserRepository.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _mockUserRepository.Verify(r => r.DeleteAsync(user), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserName = "Alice" };
            var userDto = new UserDto { UserName = "Alice" };

            _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _userService.GetByIdAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(userDto);
            _mockUserRepository.Verify(r => r.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDto = new UserDto { Id = userId, UserName = "Updated User" };
            var user = new User { Id = userId, UserName = "Updated User" };

            _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            _mockUserRepository.Setup(r => r.UpdateAsync(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateAsync(userDto);

            // Assert
            _mockUserRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
            result.Should().NotBeNull();
            result.UserName.Should().Be(userDto.UserName);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDto = new UserDto { UserName = "New User" };
            var user = new User { UserName = "New User" };
            var addedUser = new User { Id = userId, UserName = "New User" };

            _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            _mockUserRepository.Setup(r => r.AddAsync(user)).ReturnsAsync(addedUser);
            _mockMapper.Setup(m => m.Map<UserDto>(addedUser)).Returns(userDto);

            // Act
            var result = await _userService.AddAsync(userDto);

            // Assert
            result.Should().BeEquivalentTo(userDto);
            _mockUserRepository.Verify(r => r.AddAsync(user), Times.Once);
        }
    }

}
