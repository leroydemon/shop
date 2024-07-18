using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopWebApi.Controllers;
using Xunit;

namespace ShopWebApiTests.ControllerTests
{
    public class PromoCodeControllerTests
    {
        private readonly Mock<IPromoCodeService> _mockPromoCodeService;
        private readonly PromoCodeController _promoCodeController;

        public PromoCodeControllerTests()
        {
            _mockPromoCodeService = new Mock<IPromoCodeService>();
            _promoCodeController = new PromoCodeController(_mockPromoCodeService.Object);
        }
        [Fact]
        public async Task GetPromoCodeAsync_ShouldReturnOk()
        {
            // Arrange
            var promoCodeDto = new PromoCodeDto { Code = "PROMO2023", AmountDiscoint = 10 };

            _mockPromoCodeService.Setup(service => service.AddAsync(promoCodeDto)).ReturnsAsync(promoCodeDto);

            // Act
            var result = await _promoCodeController.GetPromoCodeAsync(promoCodeDto);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockPromoCodeService.Verify(service => service.AddAsync(promoCodeDto), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange
            var promoCodeId = Guid.NewGuid();

            _mockPromoCodeService.Setup(service => service.DeleteAsync(promoCodeId)).Returns(Task.CompletedTask);

            // Act
            var result = await _promoCodeController.DeleteAsync(promoCodeId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockPromoCodeService.Verify(service => service.DeleteAsync(promoCodeId), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnOk()
        {
            // Arrange
            var promoCodeDto = new PromoCodeDto { Code = "PROMO2024", AmountDiscoint = 15 };

            _mockPromoCodeService.Setup(service => service.UpdateAsync(promoCodeDto)).ReturnsAsync(promoCodeDto);

            // Act
            var result = await _promoCodeController.UpdateAsync(promoCodeDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            _mockPromoCodeService.Verify(service => service.UpdateAsync(promoCodeDto), Times.Once);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkWithPromoCode()
        {
            // Arrange
            var promoCodeId = Guid.NewGuid();
            var promoCode = new PromoCodeDto { Code = "PROMO2023", AmountDiscoint = 10 };

            _mockPromoCodeService.Setup(service => service.FindByIdAsync(promoCodeId)).ReturnsAsync(promoCode);

            // Act
            var result = await _promoCodeController.GetByIdAsync(promoCodeId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(promoCode);
        }
    }
}
