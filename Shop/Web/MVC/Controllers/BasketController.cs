using Infrastructure.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;
using System.Net;
using MVC.ViewModels;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace MVC.Controllers;

[Authorize]
public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    private readonly ILogger<BasketController> _logger;

    public BasketController(IBasketService basketService, ILogger<BasketController> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var basket = await _basketService.GetBasketGames();

        _logger.LogInformation($"Basket: {JsonConvert.SerializeObject(basket)}");
        var vm = new IndexViewModel()
        {
            BasketGames = basket.Data ?? new List<BasketGame>()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(int id, string name, decimal price)
    {
        _logger.LogInformation($"Parameters: {id} {name} {price}");
        await _basketService.AddGameToBasket(id, name, price);
        return RedirectToAction("Index", "Catalog");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromBasket(int id)
    {
        await _basketService.RemoveGameFromBasket(id);
        _logger.LogInformation($"Product id: {id}");
        return RedirectToAction("Index", "Basket");
    }
}
