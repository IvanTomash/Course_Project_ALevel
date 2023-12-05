using Basket.Host.Models.Requests;

namespace Basket.Host.Services.Interfaces;

public interface ICacheService
{
    Task AddOrUpdateAsync(string key, AddProductRequest value);

    Task RemoveAsync(string key, int productId);

    Task<T> GetAsync<T>(string key);
}
