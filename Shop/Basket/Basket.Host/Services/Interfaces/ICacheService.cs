using Basket.Host.Models.Requests;

namespace Basket.Host.Services.Interfaces;

public interface ICacheService
{
    Task<int?> AddOrUpdateAsync(string key, AddProductRequest value);

    Task<int?> RemoveAsync(string key, int productId);

    Task ClearAsync(string key);

    Task<T> GetAsync<T>(string key);
}
