using BussinessLogicLevel.Interfaces;
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
        public async Task<ActionResult> GetPromoCodeAsync(string code)
        {
            _promoCode.GetAsync(code);
            return Ok();
        }
        public async Task<ActionResult> DeleteAsync(string id)
        {
            _promoCode.DeleteAsync(id);
            return Ok();
        }
        public async Task<ActionResult> Update(string id)
        {
            _promoCode.UpdateAsync(id);
            return Ok();
        }
        public async Task<ActionResult> GetAllPromoCodesAsync()
        {
            _promoCode.GetAllAsync();
            return Ok();
        }
        public async Task<ActionResult> GetByIdAsync(string id)
        {
            _promoCode.FindByIdAsync(id);
            return Ok();
        }
    }
}


