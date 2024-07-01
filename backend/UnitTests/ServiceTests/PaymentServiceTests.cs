using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IRepository<ProductStorage>> _mockProductStorageRepo;
        private readonly Mock<IRepository<Cart>> _mockCartRepo;
        private readonly PaymentService _paymentService;
        public PaymentServiceTests()
        {
            _mockProductStorageRepo = new Mock<IRepository<ProductStorage>>();
            _mockCartRepo = new Mock<IRepository<Cart>>();
            _paymentService = new PaymentService(_mockProductStorageRepo.Object, _mockCartRepo.Object);
        }
        [Fact]
        public async Task ConfirmPurchaseAsync_ShouldReturnTrue_WhenProductsAreAvailable()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId1 = Guid.NewGuid();
            var productId2 = Guid.NewGuid();

            var cart = new Cart
            {
                Id = cartId,
                ProductList = new Dictionary<Guid, int>
            {
                { productId1, 2 },
                { productId2, 1 }
            }
            };

            var productStorage = new List<ProductStorage>
            {
            new ProductStorage { ProductId = productId1, Quantity = 5 },
            new ProductStorage { ProductId = productId2, Quantity = 3 }
            };

            _mockCartRepo.Setup(repo => repo.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockProductStorageRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productStorage);

            // Act
            var result = await _paymentService.ConfirmPurchaseAsync(cartId, 1);

            // Assert
            result.Should().BeTrue();
            _mockProductStorageRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task ConfirmPurchaseAsync_ShouldReturnFalse_WhenProductsAreNotAvailable()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId1 = Guid.NewGuid();
            var productId2 = Guid.NewGuid();

            var cart = new Cart
            {
                Id = cartId,
                ProductList = new Dictionary<Guid, int>
                {
                    { productId1, 2 },
                    { productId2, 5 }  
                }
            };

            var productStorage = new List<ProductStorage>
            {
                new ProductStorage { ProductId = productId1, Quantity = 5 },
                new ProductStorage { ProductId = productId2, Quantity = 3 }  
            };

            _mockCartRepo.Setup(repo => repo.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockProductStorageRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productStorage);

            // Act
            var result = await _paymentService.ConfirmPurchaseAsync(cartId, 1);

            // Assert
            result.Should().BeFalse();
            _mockProductStorageRepo.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
