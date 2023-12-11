using System.Net;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Data.Entities;
using Order.Host.Models.Requests;
using Order.Host.Models.Responses;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderGameController : ControllerBase
{
    private readonly ILogger<OrderGameController> _logger;
    private readonly IOrderGameService _orderGameService;

    public OrderGameController(
        ILogger<OrderGameController> logger,
        IOrderGameService orderGameService)
    {
        _logger = logger;
        _orderGameService = orderGameService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddGameRequest request)
    {
        var result = await _orderGameService.Add(request.ProductId, request.Name, request.Price, request.Count, request.OrderNumberId);
        return Ok(new ItemModifiedResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItemRequest<OrderGame> request)
    {
        var result = await _orderGameService.Update(request.Item.Id, request.Item.ProductId, request.Item.Name, request.Item.Price, request.Item.Count, request.Item.OrderNumberId);

        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteItemRequest<int> request)
    {
        var result = await _orderGameService.Delete(request.Id);
        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound(result);
    }
}
