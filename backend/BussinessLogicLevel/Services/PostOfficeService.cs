using BussinessLogicLevel.Interfaces;
using DbLevel.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BussinessLogicLevel.Services
{
    public class PostOfficeService : IPostOfficeService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _cacheSettings;
        private readonly ApiSettings _apiSettings;

        public PostOfficeService(HttpClient httpClient, IMemoryCache cache, IOptions<CacheSettings> cacheSettings, ApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
            _apiSettings = apiSettings;
        }

        public async Task<string> GetPostOfficesJsonAsync()
        {
            if (_cache.TryGetValue(_cacheSettings.PostOfficesCacheKey, out string cachedData))
            {
                return cachedData;
            }

            var response = await _httpClient.GetAsync(_apiSettings.PostOfficesApiUrl);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            _cache.Set(_cacheSettings.PostOfficesCacheKey, responseString, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_cacheSettings.CacheExpirationInDays)
            });

            return responseString;
        }
    }
}
