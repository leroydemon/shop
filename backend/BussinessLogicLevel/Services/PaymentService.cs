using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;
using System.Text.Json;

namespace BussinessLogicLevel.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<ProductStorage> _productStorageRepo;
        private readonly IRepository<Cart> _cartRepo;
        public PaymentService(IRepository<ProductStorage> productStorageRepo, IRepository<Cart> cartRepo)
        {
            _productStorageRepo = productStorageRepo;
            _cartRepo = cartRepo;
        }
        public async Task<bool> ConfirmPurchaseAsync(Guid cartId)
        {
            var cart = await _cartRepo.GetByIdAsync(cartId);
            cart.ProductList = JsonSerializer.Deserialize<Dictionary<Guid, int>>(cart.ProductListJson);
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

            cart.ProductList = new Dictionary<Guid, int>();
            cart.ProductListJson = "{}";
            cart.TotalPrice = 0;
            cart.ProductAmount = 0;

            await _cartRepo.UpdateAsync(cart);
            await _productStorageRepo.SaveChangesAsync();

            return true;
        }
    }
}