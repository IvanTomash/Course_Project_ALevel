using Basket.Host.Models;
using Basket.Host.Models.Requests;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task<GetResponse> Get(string userId);
    Task<int?> AddProduct(string userId, AddProductRequest request);
    Task<int?> RemoveProduct(string userId, int productId);
    Task ClearProducts(string userId);
}