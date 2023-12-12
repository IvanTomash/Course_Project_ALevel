using Microsoft.Extensions.Options;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using System.Text.Json;

namespace MVC.Services;

public class OrderService : IOrderService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<OrderService> _logger;
    private readonly IBasketService _basketService;

    public OrderService(
        IHttpClientService httpClient,
        ILogger<OrderService> logger,
        IOptions<AppSettings> settings,
        IBasketService basketService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings;
        _basketService = basketService;
    }

    public async Task<int?> CreateOrder()
    {
        var result = await _httpClient.SendAsync<int?, Basket>($"{_settings.Value.OrderUrl}/CreateOrder",
            HttpMethod.Post,
            await _basketService.GetBasketGames());
        _logger.LogInformation($"CreateOrder: {result}");
        return result;
    }

    public async Task<Order> GetOrders()
    {
        var result = await _httpClient.SendAsync<Order, object>($"{_settings.Value.OrderUrl}/GetOrders",
             HttpMethod.Post, null);
        _logger.LogInformation($"Order: {JsonSerializer.Serialize(result)}");
        if ( result == null ) 
        {
        }
        return result;
    }
}
