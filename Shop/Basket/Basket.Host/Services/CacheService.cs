using Basket.Host.Configurations;
using Basket.Host.Models.Requests;
using Basket.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Basket.Host.Services;

public class CacheService : ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly IRedisCacheConnectionService _redisCacheConnectionService;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly RedisConfig _config;

    public CacheService(
        ILogger<CacheService> logger,
        IRedisCacheConnectionService redisCacheConnectionService,
        IOptions<RedisConfig> config,
        IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _redisCacheConnectionService = redisCacheConnectionService;
        _jsonSerializer = jsonSerializer;
        _config = config.Value;
    }

    public Task AddOrUpdateAsync(string key, AddProductRequest value)
        => AddOrUpdateInternalAsync(key, value);

    private async Task AddOrUpdateInternalAsync(string key, AddProductRequest value,
       IDatabase redis = null!, TimeSpan? expiry = null)
    {
        redis = redis ?? GetRedisDatabase();
        expiry = expiry ?? _config.CacheTimeout;

        var cahceKey = GetItemCacheKey(key);

        var products = await GetAsync<List<AddProductRequest>>(key);
       
        if (products == null)
        {
            products = new List<AddProductRequest>();  
        }

        var certainProduct = products.FirstOrDefault(x => x.Id == value.Id);
        if (certainProduct != null)
        {
            value.Count += certainProduct.Count;
            products.Remove(certainProduct);
        }
       
        products.Add(value);

        var serialized = _jsonSerializer.Serialize(products);

        if (await redis.StringSetAsync(cahceKey, serialized, expiry))
        {
            _logger.LogInformation($"Cached value for key {key} cached");
        }
        else
        {
            _logger.LogInformation($"Cached value for key {key} updated");
        }
    }

    public Task RemoveAsync(string key, int productId)
       => RemoveInternalAsync(key, productId);

    private async Task RemoveInternalAsync(string key, int productId,
       IDatabase redis = null!, TimeSpan? expiry = null)
    {
        redis = redis ?? GetRedisDatabase();
        expiry = expiry ?? _config.CacheTimeout;

        var cahceKey = GetItemCacheKey(key);

        var products = await GetAsync<List<AddProductRequest>>(key);

        if (products == null)
        {
            products = new List<AddProductRequest>();
        }

        var certainProduct = products.FirstOrDefault(x => x.Id == productId);

        if (certainProduct != null)
        {
            var updatedProduct = certainProduct;
            updatedProduct.Count -= 1;

            products.Remove(certainProduct);
            if (updatedProduct.Count > 0)
            {
                products.Add(updatedProduct);
            }
        }

        var serialized = _jsonSerializer.Serialize(products);

        if (await redis.StringSetAsync(cahceKey, serialized, expiry))
        {
            _logger.LogInformation($"Cached value for key {key} cached");
        }
        else
        {
            _logger.LogInformation($"Cached value for key {key} updated");
        }
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var redis = GetRedisDatabase();

        var cacheKey = GetItemCacheKey(key);

        var serialized = await redis.StringGetAsync(cacheKey);

        return serialized.HasValue ?
            _jsonSerializer.Deserialize<T>(serialized.ToString())
            : default(T)!;
    }

    private string GetItemCacheKey(string userId) =>
        $"{userId}";

    private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
}
