using BussinessLogicLevel.DtoModels;
using BussinessLogicLevel.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _accountController = new AccountController(_mockAccountService.Object);
        }
        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@example.com", Password = "Password123" };
            _mockAccountService.Setup(service => service.Register(registerDto)).ReturnsAsync(true);

            // Act
            var result = await _accountController.Register(registerDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(true);
            _mockAccountService.Verify(service => service.Register(registerDto), Times.Once);
        }
        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@example.com", Password = "Password123" };
            _mockAccountService.Setup(service => service.Register(registerDto)).ReturnsAsync(false);

            // Act
            var result = await _accountController.Register(registerDto);

            // Assert
            var badRequestResult = result as BadRequestResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            _mockAccountService.Verify(service => service.Register(registerDto), Times.Once);
        }
        [Fact]
        public async Task Login_ShouldReturnOkWithToken()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };
            var token = "test-token";
            _mockAccountService.Setup(service => service.Login(loginDto)).ReturnsAsync(token);

            // Act
            var result = await _accountController.Login(loginDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(token);
            _mockAccountService.Verify(service => service.Login(loginDto), Times.Once);
        }
        [Fact]
        public async Task LogOut_ShouldReturnOk_WhenLogOutIsSuccessful()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockAccountService.Setup(service => service.LogOut(userId)).ReturnsAsync(true);

            // Act
            var result = await _accountController.LogOut(userId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockAccountService.Verify(service => service.LogOut(userId), Times.Once);
        }
        [Fact]
        public async Task LogOut_ShouldReturnBadRequest_WhenLogOutFails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockAccountService.Setup(service => service.LogOut(userId)).ReturnsAsync(false);

            // Act
            var result = await _accountController.LogOut(userId);

            // Assert
            var badRequestResult = result as BadRequestResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            _mockAccountService.Verify(service => service.LogOut(userId), Times.Once);
        }
    }
}
