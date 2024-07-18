using BussinessLogicLevel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        [HttpPost]
        public async Task<IActionResult> AddToAsync(Guid cartId, Guid productId, int quantity)
        {
            var cart = await _cartService.AddToAsync(cartId, productId, quantity);

            return Ok(cart);
        }
        [HttpGet("{cartId}/products")]
        public async Task<IActionResult> ItemInCart(Guid cartId)
        {
            var items = await _cartService.ItemInCart(cartId);

            return Ok(items);
        }
        [HttpDelete("{cartId}/clear")]
        public async Task<IActionResult> ClearAsync(Guid cartId)
        {
            await _cartService.ClearAsync(cartId);

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid cartId)
        {
            var cart = await _cartService.GetAsync(cartId);

            return Ok(cart);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromAsync(Guid cartId, Guid productId, int quantity)
        {
            await _cartService.RemoveFromAsync(cartId, productId, quantity);

            return Ok();
        }
    }
}
