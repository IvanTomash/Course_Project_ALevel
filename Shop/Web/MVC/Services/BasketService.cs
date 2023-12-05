using Microsoft.Extensions.Options;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;
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

    public async Task AddGameToBasket(int id, string name, decimal price)
    {
        await _httpClient.SendAsync<object, BasketGame>($"{_settings.Value.BasketUrl}/AddProduct", 
            HttpMethod.Post,
            new BasketGame()
            {
               Id = id,
               Name = name,
               Price = price,
               Count = 1
            });
    }

    public async Task<Basket> GetBasketGames()
    {
        var result = await _httpClient.SendAsync<Basket, object>($"{_settings.Value.BasketUrl}/GetProducts", HttpMethod.Post, null);
        return result;
    }

    public async Task RemoveGameFromBasket(int id)
    {
        _logger.LogInformation($"Basket service product id: {id}");
        await _httpClient.SendAsync<object, RemoveProductRequest>($"{_settings.Value.BasketUrl}/RemoveProduct", HttpMethod.Post, new RemoveProductRequest() { Id = id});
    }
}
