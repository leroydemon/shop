using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopWebApi.Controllers;
using Xunit;
using BussinessLogicLevel.Interfaces;

namespace ShopWebApiTests.ControllerTests
{
    public class PaymentControllerTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly PaymentController _paymentController;
        public PaymentControllerTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _paymentController = new PaymentController(_mockPaymentService.Object);
        }
        [Fact]
        public async Task ConfirmPurchaseAsync_ShouldReturnOk()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var quantity = 1;
            var confirmationResult = true;

            _mockPaymentService.Setup(service => service.ConfirmPurchaseAsync(cartId, quantity))
               .ReturnsAsync(confirmationResult);

            // Act
            var result = await _paymentController.ConfirmPurchaseAsync(cartId, quantity);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(confirmationResult);
            _mockPaymentService.Verify(service => service.ConfirmPurchaseAsync(cartId, quantity), Times.Once);
        }
    }
}
