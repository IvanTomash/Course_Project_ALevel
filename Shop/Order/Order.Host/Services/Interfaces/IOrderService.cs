using Microsoft.AspNetCore.Http.Features;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Responses;

namespace Order.Host.Services.Interfaces;

public interface IOrderService
{
    Task<ItemsResponse<OrderGameDto>> GetOrderGamesAsync();
    Task<ItemsResponse<OrderNumberDto>> GetOrderNumbersAsync();
    Task<int?> CreateOrderAsync(CreateOrderRequest request);
    Task<OrderResponse> GetOrdersAsync();
}
