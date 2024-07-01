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

            var productStorage = await _productStorageRepo.GetAllAsync();

            bool isConfirmed = true;

            foreach (var item in cart.ProductList)
            {
                foreach(var product in productStorage)
                {
                    if (item.Key == product.ProductId && item.Value <= product.Quantity)
                    {
                        product.Quantity -= item.Value;
                        break;
                    }
                    else if (item.Key == product.ProductId && item.Value > product.Quantity)
                    {
                        product.Quantity -= item.Value; // высылать сообщение сторажу что нужно н-ое кол-во товара
                    }
                    else
                    {
                        return isConfirmed = false;
                    }
                }
            }
            return isConfirmed;
        }
    }
}
