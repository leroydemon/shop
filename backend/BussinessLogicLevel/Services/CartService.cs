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
        // Task<Cart>
        public async Task<CartDto> AddToAsync(Guid cartId, Guid productId, int quantity)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var product = await _productRepo.GetByIdAsync(productId);

            var productList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);

            if (!productList.ContainsKey(product.Id))
            {
                productList.Add(product.Id, quantity);
            }
            else
            {
                productList[product.Id] += quantity;
            }
            cart.TotalPrice += product.UnitPrice * quantity;
            cart.ProductAmount = productList.Count;
            cart.ProductListJson = JsonSerializer.Serialize(productList);

            await _cartRepository.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
        }

        public async Task ClearAsync(Guid cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            cart.ProductListJson = "";
            cart.ProductList = new Dictionary<Guid, int>();
            cart.TotalPrice = 0;
            cart.ProductAmount = 0;
            await _cartRepository.SaveChangesAsync();
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
            var productList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);

            if (productList.ContainsKey(productId))
            {
                var itemQuantity = productList[productId];
                var finalQuantity = itemQuantity - quantity;
                if (finalQuantity <= 0)
                {
                    productList.Remove(productId);
                    cart.TotalPrice -= product.UnitPrice * itemQuantity;
                }
                else
                {
                    productList[productId] = finalQuantity;
                    cart.TotalPrice -= product.UnitPrice * quantity;
                }
                cart.TotalPrice -= product.UnitPrice * finalQuantity;
                cart.ProductAmount = productList.Count;
            }
            cart.ProductListJson = JsonSerializer.Serialize(productList);
            await _cartRepository.SaveChangesAsync();
        }
        public async Task<Cart> CreateCart(Guid userId)
        {
            var cart =  new Cart()
            {
                UserId = userId
            };
            return await _cartRepository.AddAsync(cart);
        }
        public async Task SaveChangesAsync()
        {
            await _cartRepository.SaveChangesAsync();
        }
    }
}
