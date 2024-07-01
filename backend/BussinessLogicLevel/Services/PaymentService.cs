using DbLevel.Interfaces;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class PaymentService
    {
        private readonly IRepository<ProductStorage> _productStorageRepo;
        private readonly IRepository<Cart> _cartRepo;
        public PaymentService(IRepository<ProductStorage> productStorageRepo, IRepository<Cart> cartRepo)
        {
            _productStorageRepo = productStorageRepo;
            _cartRepo = cartRepo;
        }
        public async Task<bool> ConfirmPurchaseAsync(Guid cartId, int quantity)
        {
            var cart = await _cartRepo.GetByIdAsync(cartId);
            var productStorageList = await _productStorageRepo.GetAllAsync();

            foreach (var cartItem in cart.ProductList)
            {
                var productStorage = productStorageList.FirstOrDefault(ps => ps.ProductId == cartItem.Key);
                if (productStorage == null || productStorage.Quantity < cartItem.Value)
                {
                    return false;
                }
            }

            foreach (var cartItem in cart.ProductList)
            {
                var productStorage = productStorageList.First(ps => ps.ProductId == cartItem.Key);
                productStorage.Quantity -= cartItem.Value;
            }

            await _productStorageRepo.SaveChangesAsync();
            return true;
        }
    }
}
