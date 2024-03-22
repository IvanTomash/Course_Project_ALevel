using Microsoft.Extensions.Options;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace MVC.Services;

public class BasketService : IBasketService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        IHttpClientService httpClient,
        ILogger<BasketService> logger,
        IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings;
    }

    public async Task<int?> AddGameToBasket(int id, string name, decimal price)
    {
        var result = await _httpClient.SendAsync<int?, BasketGame>($"{_settings.Value.BasketUrl}/AddProduct", 
            HttpMethod.Post,
            new BasketGame()
            {
               Id = id,
               Name = name,
               Price = price,
               Count = 1
            });

        return result;
    }

    public async Task<Basket> GetBasketGames()
    {
        var result = await _httpClient.SendAsync<Basket, object>($"{_settings.Value.BasketUrl}/GetProducts", HttpMethod.Post, null);
        _logger.LogInformation($"Basket: {JsonConvert.SerializeObject(result)}");
        return result;
    }

    public async Task<int?> RemoveGameFromBasket(int id)
    {
        _logger.LogInformation($"Basket service product id: {id}");
        var result = await _httpClient.SendAsync<int?, RemoveProductRequest>($"{_settings.Value.BasketUrl}/RemoveProduct", HttpMethod.Post, new RemoveProductRequest() { Id = id});
        return result;
    }

    public async Task ClearBasket()
    {
        await _httpClient.SendAsync<object, object>($"{_settings.Value.BasketUrl}/ClearProducts", HttpMethod.Post, null);
    }
}
