using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;
using System.Text.Json;

namespace BussinessLogicLevel.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        public CartService(IRepository<Cart> cartRepository, IRepository<Product> productService, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepo = productService;
            _mapper = mapper;
        }
        public async Task<CartDto> AddToAsync(Guid cartId, Guid productId, int quantity)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var product = await _productRepo.GetByIdAsync(productId);

            if (!cart.ProductList.ContainsKey(product.Id))
            {
                cart.ProductList.Add(product.Id, quantity);
            }
            else
            {
                cart.ProductList[product.Id] += quantity;
            }
            cart.ProductListJson = JsonSerializer.Serialize(cart.ProductList);
            cart.TotalPrice += product.UnitPrice * quantity;
            cart.ProductAmount = cart.ProductList.Count;

            await _cartRepository.UpdateAsync(cart);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task ClearAsync(Guid cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            cart.ProductList = new Dictionary<Guid, int>();
            cart.ProductListJson = "{}";
            cart.TotalPrice = 0;
            cart.ProductAmount = 0;

            await _cartRepository.UpdateAsync(cart);
        }

        public async Task<CartDto> GetAsync(Guid cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task RemoveFromAsync(Guid cartId, Guid productId, int quantity)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var product = await _productRepo.GetByIdAsync(productId);

            if (cart.ProductList.ContainsKey(productId))
            {
                var itemQuantity = cart.ProductList[productId];
                var finalQuantity = itemQuantity - quantity;
                if (finalQuantity <= 0)
                {
                    cart.ProductList.Remove(productId);
                    cart.TotalPrice -= product.UnitPrice * itemQuantity;
                }
                else
                {
                    cart.ProductList[productId] = finalQuantity;
                    cart.TotalPrice -= product.UnitPrice * quantity;
                }
                cart.ProductAmount = cart.ProductList.Count;
                cart.ProductListJson = JsonSerializer.Serialize(cart.ProductList);
            }
            await _cartRepository.UpdateAsync(cart);
        }
        public async Task<Cart> CreateCartAsync(Guid userId)
        {
            var cart = new Cart()
            {
                UserId = userId
            };

            return await _cartRepository.AddAsync(cart);
        }
        public async Task<Dictionary<Guid, int>> ItemInCart(Guid userId)
        {
            var cart = await _cartRepository.GetByIdAsync(userId);
            if (!string.IsNullOrEmpty(cart.ProductListJson))
            {
                cart.ProductList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);
            }
            return cart.ProductList;
        }
    }
}
