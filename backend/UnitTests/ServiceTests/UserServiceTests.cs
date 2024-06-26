﻿using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Infrastucture.DtoModels;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetSortedAsync_ShouldReturnSortedUserDtos()
        {
            // Arrange

            var users = new List<User>
            {
                new User { Id = Guid.NewGuid().ToString(), UserName = "Alice" },
                new User { Id = Guid.NewGuid().ToString(), UserName = "Bob" }
            };
            var userDtos = new List<UserDto>
            {
                new UserDto { Name = "Alice" },
                new UserDto { Name = "Bob" }
            };

            _mockUserRepository.Setup(r => r.GetSortedAsync("A", 1, 10, "UserName", true)).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _userService.GetSortedAsync("A", 1, 10, "UserName", true);

            // Assert
            result.Should().BeEquivalentTo(userDtos);
            _mockUserRepository.Verify(r => r.GetSortedAsync("A", 1, 10, "UserName", true), Times.Once);
        }

        [Fact]
        public async Task SetOnlineAsync_ShouldCallRepositorySetOnline()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId.ToString(), IsOnline = false };

            _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.SetOnlineAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.SetOnlineAsync(userId);

            // Assert
            _mockUserRepository.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _mockUserRepository.Verify(r => r.SetOnlineAsync(user), Times.Once);
        }

        [Fact]
        public async Task SetOfflineAsync_ShouldCallRepositorySetOffline()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId.ToString(), IsOnline = true };

            _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.SetOfflineAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.SetOfflineAsync(userId);

            // Assert
            _mockUserRepository.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _mockUserRepository.Verify(r => r.SetOfflineAsync(user), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId.ToString() };

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
            var user = new User { Id = userId.ToString(), UserName = "Alice" };
            var userDto = new UserDto { Name = "Alice" };

            _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _userService.GetByIdAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(userDto);
            _mockUserRepository.Verify(r => r.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserDtos()
        {
            // Arrange
            var id = Guid.NewGuid();
            var users = new List<User>
            {
                new User { Id = id.ToString(), UserName = "Alice" },
                new User { Id = id.ToString(), UserName = "Bob" }
            };
            var userDtos = new List<UserDto>
            {
                new UserDto { Name = "Alice" },
                new UserDto { Name = "Bob" }
            };

            _mockUserRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(userDtos);
            _mockUserRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDto = new User { Name = "Updated User" };
            var user = new User { Id = userId.ToString(), UserName = "Updated User" };

            _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            _mockUserRepository.Setup(r => r.UpdateAsync(user)).Returns(Task.CompletedTask);

            // Act
            await _userService.UpdateAsync(userDto);

            // Assert
            _mockUserRepository.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDto = new UserDto { Name = "New User" };
            var user = new User { UserName = "New User" };
            var addedUser = new User { Id = userId.ToString(), UserName = "New User" };

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
