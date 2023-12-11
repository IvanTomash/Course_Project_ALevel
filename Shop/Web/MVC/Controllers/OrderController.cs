using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var order = await _orderService.GetOrders();
        _logger.LogInformation($"Order: {order}");
        var vm = new IndexViewModel()
        {
            OrderGames = order.OrderGames ?? new List<OrderGame>(),
            OrderNumbers = order.OrderNumbers ?? new List<OrderNumber>()
        };
        _logger.LogInformation($"Order vm: {vm}");
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        var result = await _orderService.CreateOrder();
        return RedirectToAction("Index", "Basket");
    }
}
