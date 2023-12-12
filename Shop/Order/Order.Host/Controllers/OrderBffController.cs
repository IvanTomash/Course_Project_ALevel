using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Responses;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderBffController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IOrderService _orderService;

    public OrderBffController(
        ILogger<OrderBffController> logger,
        IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult> GetGames()
    {
        var result = await _orderService.GetOrderGamesAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> GetNumbers()
    {
        var result = await _orderService.GetOrderNumbersAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderRequest request)
    {
        var personId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var result = await _orderService.CreateOrderAsync(request, personId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> GetOrders()
    {
        var personId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var orders = await _orderService.GetOrdersAsync();
        orders.OrderNumbers = orders.OrderNumbers.Where(s => s.PersonId == personId).ToList();
        orders.OrderGames = orders.OrderGames.Where(s => s.OrderNumber.PersonId == personId).ToList();

        _logger.LogInformation($"Person id: {personId}. Order numbers: {orders.OrderNumbers}. Order games: {orders.OrderGames}");
        return Ok(orders);
    }
}
