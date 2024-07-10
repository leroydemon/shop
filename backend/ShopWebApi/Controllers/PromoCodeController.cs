using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeService _promoCode;
        public PromoCodeController(IPromoCodeService promoCode)
        {
            _promoCode = promoCode;
        }
        [HttpPost]
        public async Task<ActionResult> GetPromoCodeAsync(PromoCodeDto promoCode)
        {
            await _promoCode.AddAsync(promoCode);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _promoCode.DeleteAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(PromoCodeDto promoCode)
        {
            var updatedPromoCode = await _promoCode.UpdateAsync(promoCode);
            return Ok(updatedPromoCode);
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllPromoCodesAsync()
        {
            var items = await _promoCode.GetAllAsync();
            return Ok(items);
        }
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var item = await _promoCode.FindByIdAsync(id);
            return Ok(item);
        }
    }
}


