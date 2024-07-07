using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;

namespace BussinessLogicLevel.Services
{
    public class PaymentService
    {
        private readonly IRepository<ProductStorage> _productStorageRepo;
        private readonly IRepository<Cart> _cartRepo;
        public PaymentService(Repository<ProductStorage> productStorageRepo, IRepository<Cart> cartRepo)
        {
            _productStorageRepo = productStorageRepo;
            _cartRepo = cartRepo;
        }
        public async Task<bool> ConfirmPurchaseAsync(Guid cartId, int quantity)
        {
            var cart = await _cartRepo.GetByIdAsync(cartId);
            var productIds = cart.ProductList.Keys;

            var specification = new ProductStorageByProductIdsSpecification(productIds);
            var productStorageList = await _productStorageRepo.ListAsync(specification);

            var productStorageDict = productStorageList
                .GroupBy(ps => ps.ProductId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var cartItem in cart.ProductList)
            {
                if (!productStorageDict.TryGetValue(cartItem.Key, out var storages) || storages.Sum(s => s.Quantity) < cartItem.Value)
                {
                    return false;
                }
            }

            foreach (var cartItem in cart.ProductList)
            {
                var storages = productStorageDict[cartItem.Key];
                int quantityToDeduct = cartItem.Value;

                foreach (var storage in storages)
                {
                    if (storage.Quantity >= quantityToDeduct)
                    {
                        storage.Quantity -= quantityToDeduct;
                        storage.UpdatedDateTime = DateTime.Now;
                        break;
                    }
                    else
                    {
                        quantityToDeduct -= storage.Quantity;
                        storage.Quantity = 0;
                        storage.UpdatedDateTime = DateTime.Now;
                    }
                }
            }

            await _productStorageRepo.SaveChangesAsync();

            return true;
        }
    }
}