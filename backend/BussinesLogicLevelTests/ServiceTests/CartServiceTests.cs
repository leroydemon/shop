using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using System.Text.Json;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class CartServiceTests
    {
        private readonly Mock<IRepository<Cart>> _mockCartRepository;
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly CartService _cartService;
        private readonly Mock<IMapper> _mockMapper;

        public CartServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockCartRepository = new Mock<IRepository<Cart>>();
            _mockProductRepository = new Mock<IRepository<Product>>();
            _cartService = new CartService(_mockCartRepository.Object,
                _mockProductRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task AddToAsync_ShouldAddNewProductToCart_ReturnCartDto()
        {
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 2;

            var cart = new Cart
            {
                Id = cartId,
                ProductListJson = "{}",
                TotalPrice = 0,
                ProductAmount = 0
            };

            var product = new Product
            {
                Id = productId,
                UnitPrice = 10
            };

            var updatedProductList = new Dictionary<Guid, int> { { productId, quantity } };

            _mockCartRepository.Setup(r => r.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<CartDto>(cart)).Returns(new CartDto());

            // Act
            var result = await _cartService.AddToAsync(cartId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.TotalPrice.Should().Be(20);
            _mockCartRepository.Verify(r => r.GetByIdAsync(cartId), Times.Once);
            _mockProductRepository.Verify(r => r.GetByIdAsync(productId), Times.Once);
            _mockCartRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

            var deserializedProductList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);
            deserializedProductList.Should().ContainKey(productId);
            deserializedProductList[productId].Should().Be(quantity);
        }

        [Fact]
        public async Task AddToAsync_ShouldUpdateExistingProductQuantityInCart_ReturnCartDto()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var initialQuantity = 2;
            var additionalQuantity = 3;
            var existingProductList = new Dictionary<Guid, int> { { productId, initialQuantity } };

            var cart = new Cart
            {
                Id = cartId,
                ProductListJson = JsonSerializer.Serialize(existingProductList),
                TotalPrice = 20,
                ProductAmount = 1
            };

            var product = new Product
            {
                Id = productId,
                UnitPrice = 10
            };

            var updatedQuantity = initialQuantity + additionalQuantity;
            var updatedProductList = new Dictionary<Guid, int> { { productId, updatedQuantity } };
            var cartDto = new Cart
            {
                TotalPrice = product.UnitPrice * updatedQuantity,
                ProductAmount = 1
            };

            _mockCartRepository.Setup(r => r.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockCartRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockCartRepository.Setup(r => r.GetByIdAsync(cartId)).ReturnsAsync(cart);

            // Act
            var result = await _cartService.AddToAsync(cartId, productId, additionalQuantity);

            // Assert
            result.Should().NotBeNull();
            result.TotalPrice.Should().Be(50);
            result.ProductAmount.Should().Be(1);
            _mockCartRepository.Verify(r => r.GetByIdAsync(cartId), Times.Once);
            _mockProductRepository.Verify(r => r.GetByIdAsync(productId), Times.Once);
            _mockCartRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ClearAsync_ClearCart()
        {
            //Arrange
            var cartId = Guid.NewGuid();
            var productList = new Dictionary<Guid, int>
            {
               { Guid.NewGuid(), 2 },
               { Guid.NewGuid(), 3 }
            };
            var cart = new Cart
            {
                UserId = cartId,
                ProductAmount = 2,
                ProductList = productList,
                TotalPrice = 100,
                ProductListJson = JsonSerializer.Serialize(productList),
                Id = cartId
            };
            _mockCartRepository.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockCartRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            //Act
            await _cartService.ClearAsync(cartId);
            //Assert
            _mockCartRepository.Verify(x => x.GetByIdAsync(cartId), Times.Once);
            _mockCartRepository.Verify(x => x.SaveChangesAsync(), Times.Once);

            cart.ProductListJson.Should().Be("{}");
            cart.ProductAmount.Should().Be(0);
            cart.ProductList.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAsync_GetCartById_ReturnCartDto()
        {
            //Arrange
            var cartId = Guid.NewGuid();
            Cart cart = new Cart { Id = cartId, UserId = Guid.NewGuid() };
            CartDto cartDto = new CartDto { UserId = cart.UserId };

            _mockCartRepository.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockMapper.Setup(x => x.Map<CartDto>(cart)).Returns(cartDto);

            //Act
            var results = await _cartService.GetAsync(cartId);

            //Assert
            results.Should().NotBeNull();
            results.Should().BeSameAs(cartDto);
            results.UserId.Should().Be(cart.UserId);
            _mockCartRepository.Verify(x => x.GetByIdAsync(cartId), Times.Once);
        }

        [Fact]
        public async Task RemoveAllFromAsync_RemoveAllItemsFromCart()
        {
            //Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            int quantity = 1;
            var productList = new Dictionary<Guid, int>()
            {
                { productId, 1 }
            };
            var cart = new Cart
            {
                Id = cartId,
                ProductAmount = 1,
                ProductList = productList,
                TotalPrice = 10,
                ProductListJson = JsonSerializer.Serialize(productList)
            };
            var product = new Product
            {
                Id = productId,
                UnitPrice = 10,
            };

            _mockCartRepository.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockProductRepository.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockCartRepository.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _cartService.RemoveFromAsync(cartId, productId, quantity);

            //Assert
            _mockCartRepository.Verify(x => x.GetByIdAsync(cartId), Times.Once);
            _mockProductRepository.Verify(x => x.GetByIdAsync(productId), Times.Once);
            _mockCartRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
            cart.TotalPrice.Should().Be(0);
            cart.ProductAmount.Should().Be(0);
            var deserializedList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);
            deserializedList.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task RemoveFromAsync_ShouldUpdateExistingProductQuantityInCart()
        {
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            int quantity = 1;
            var productList = new Dictionary<Guid, int>()
            {
                { productId, 2 }
            };
            var cart = new Cart
            {
                Id = cartId,
                ProductAmount = 2,
                ProductList = productList,
                TotalPrice = 20,
                ProductListJson = JsonSerializer.Serialize(productList)
            };
            var product = new Product
            {
                Id = productId,
                UnitPrice = 10,
            };

            _mockCartRepository.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _mockProductRepository.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockCartRepository.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _cartService.RemoveFromAsync(cartId, productId, quantity);

            //Assert
            _mockCartRepository.Verify(x => x.GetByIdAsync(cartId), Times.Once);
            _mockProductRepository.Verify(x => x.GetByIdAsync(productId), Times.Once);
            _mockCartRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
            cart.TotalPrice.Should().Be(10);
            cart.ProductAmount.Should().Be(1);
            var deserializedList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);
            deserializedList.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task CreateCart_ShouldCreateCart_ReturnCart()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var cart = new Cart { Id = Guid.NewGuid(), UserId = userId };

            _mockCartRepository.Setup(x => x.AddAsync(It.IsAny<Cart>())).ReturnsAsync((Cart c) => c);

            //Act
            var result = await _cartService.CreateCartAsync(userId);

            //Assert
            result.Should().NotBeNull();
            _mockCartRepository.Verify(x => x.AddAsync(It.Is<Cart>(c => c.UserId == userId)), Times.Once);
        }

        [Fact]
        public async Task SaveChangesAsync_SaveChangesInDb()
        {
            //Arrange
            _mockCartRepository.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            //Act
            await _cartService.SaveChangesAsync();

            //Assert
            _mockCartRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
