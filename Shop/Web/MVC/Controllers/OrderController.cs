using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.OrderViewModels;
using System.Text.Json;

namespace MVC.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IBasketService _basketService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger, IBasketService basketService)
    {
        _orderService = orderService;
        _logger = logger;
        _basketService = basketService;
    }

    public async Task<IActionResult> Index()
    {
        var order = await _orderService.GetOrders();
        var vm = new IndexViewModel();
        if (order != null)
        {
            vm = new IndexViewModel()
            {
                OrderGames = order.OrderGames ?? new List<OrderGame>(),
                OrderNumbers = order.OrderNumbers ?? new List<OrderNumber>()
            };
            _logger.LogInformation($"Order vm: {JsonSerializer.Serialize(vm)}");
            return View(vm);
        }

        vm = new IndexViewModel()
        {
            OrderGames =  new List<OrderGame>(),
            OrderNumbers = new List<OrderNumber>()
        };
        _logger.LogInformation($"Order vm: {JsonSerializer.Serialize(vm)}");
        return View(vm);
       
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        var result = await _orderService.CreateOrder();
        await _basketService.ClearBasket();
        _logger.LogInformation($"Baket is empty. Creted order with number {result}");
        return RedirectToAction("Index", "Basket");
    }
}
