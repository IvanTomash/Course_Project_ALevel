using Basket.Host.Models;
using Basket.Host.Models.Requests;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task<GetResponse> Get(string userId);
    Task AddProduct(string userId, AddProductRequest request);
    Task RemoveProduct(string userId, int productId);
}