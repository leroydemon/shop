using BussinessLogicLevel.Interfaces;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BussinessLogicLevel.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<ProductStorage> _productStorageRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;
        private readonly IPostOfficeService _postOffice;
        private readonly IRepository<Order> _orderRepo;
        public PaymentService(
            IRepository<ProductStorage>
            pSRepo,
            IRepository<Cart> cartRepo,
            IRepository<User> userRepo,
            IMemoryCache memoryCache,
            CacheSettings cacheSettings,
            IRepository<Order> orderRepo,
            IPostOfficeService postOffice)
        {
            _productStorageRepo = pSRepo;
            _cartRepo = cartRepo;
            _userRepo = userRepo;
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings;
            _orderRepo = orderRepo;
            _postOffice = postOffice;
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

            var user = await _userRepo.GetByIdAsync(cart.UserId);

            var order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerName = user.UserName,
                ProductListJson = cart.ProductListJson,

            };

            await _orderRepo.AddAsync(order);
            await _cartRepo.UpdateAsync(cart);
            await _productStorageRepo.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<PostOffice>> GetNearbyPostOffice(Guid userId, int maxResults)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            IEnumerable<PostOffice> postOffices = new List<PostOffice>();
            double minDistance = double.MaxValue;
            PostOffice nearestPostOffice = null;
            var postOfficesWithDistance = new List<(PostOffice PostOffice, double Distance)>();

            if (_memoryCache.TryGetValue(_cacheSettings.PostOfficesCacheKey, out string cachedData))
            {
                postOffices = JsonSerializer.Deserialize<IEnumerable<PostOffice>>(cachedData);

                foreach (var postOffice in postOffices)
                {
                    double distance = GetDistance(user.Latitude, user.Longitude, postOffice.Latitude, postOffice.Longitude);
                    postOfficesWithDistance.Add((postOffice, distance));
                }
                var nearestPostOffices = postOfficesWithDistance
                            .OrderBy(p => p.Distance)
                            .Take(maxResults)
                            .Select(p => p.PostOffice);

                return nearestPostOffices;
            }
            else
            {
                var postOfficesJson = await _postOffice.GetPostOfficesJsonAsync();
                postOffices = JsonSerializer.Deserialize<IEnumerable<PostOffice>>(postOfficesJson);

                _memoryCache.Set(_cacheSettings.PostOfficesCacheKey, postOfficesJson, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_cacheSettings.CacheExpirationInDays)
                });
            }

            return postOffices;
        }
        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371e3; 
            var φ1 = lat1 * Math.PI / 180; 
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = R * c;
            return distance;
        }
    }
}