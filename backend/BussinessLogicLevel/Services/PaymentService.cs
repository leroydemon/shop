using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Settings;
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
                User = user,
                ProductListJson = cart.ProductListJson,

            };

            await _orderRepo.AddAsync(order);
            await _cartRepo.UpdateAsync(cart);
            await _productStorageRepo.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<PostOffice>> GetNearestPostOfficesAsync(Guid userId, int maxResults)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            IEnumerable<PostOffice> postOffices;

            if (_memoryCache.TryGetValue(_cacheSettings.PostOfficesCacheKey, out string cachedData))
            {
                postOffices = JsonSerializer.Deserialize<IEnumerable<PostOffice>>(cachedData);
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

            return GetNearestPostOffices(postOffices, user, maxResults);
        }

        private IEnumerable<PostOffice> GetNearestPostOffices(IEnumerable<PostOffice> postOffices, User user, int maxResults)
        {
            var postOfficesWithDistance = new List<(PostOffice PostOffice, double Distance)>();

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
        private double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            const double EarthRadius = 6371e3;

            var latitude1Rad = latitude1 * Math.PI / 180;
            var latitude2Rad = latitude2 * Math.PI / 180;
            var deltaLatitude = (latitude2 - latitude1) * Math.PI / 180;
            var deltaLongitude = (longitude2 - longitude1) * Math.PI / 180;

            var a = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
                    Math.Cos(latitude1Rad) * Math.Cos(latitude2Rad) *
                    Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = EarthRadius * c;

            return distance;
        }
    }
}
