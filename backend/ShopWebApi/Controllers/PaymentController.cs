using BussinessLogicLevel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmPurchaseAsync(Guid cartId, int quantity)
        {
            var confirmationResult = await _paymentService.ConfirmPurchaseAsync(cartId, quantity);
            return Ok(confirmationResult);
        }
    }
}
