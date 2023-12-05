using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;

    public BasketService(ICacheService cacheService)
    {
        this._cacheService = cacheService;
    }

    public async Task<GetResponse> Get(string userId)
    {
        var result = await _cacheService.GetAsync<List<AddProductRequest>>(userId);
        return new GetResponse() { Data = result };
    }

    public async Task AddProduct(string userId, AddProductRequest request)
    {
        await _cacheService.AddOrUpdateAsync(userId, request);
    }

    public async Task RemoveProduct(string userId, int productId)
    {
        await _cacheService.RemoveAsync(userId, productId);
    }
}