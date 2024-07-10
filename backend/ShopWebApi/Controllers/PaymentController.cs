using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;

        [HttpGet]
        public async Task<IActionResult> ConfirmPurchaseAsync(Guid cartId)
        {
            var confirmationResult = await _paymentService.ConfirmPurchaseAsync(cartId);
            return Ok(confirmationResult);
        }
        [HttpGet("\"Payment/{userId}/nearby-post-offices\"")]
        public async Task<IActionResult> GetNearbyPostOffice(Guid userId, int maxResults)
        {
            return Ok(await _paymentService.GetNearbyPostOffice(userId, maxResults));
        }
    }
}
