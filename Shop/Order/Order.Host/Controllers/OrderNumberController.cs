using System.Net;
using System.Runtime.CompilerServices;
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
public class OrderNumberController : ControllerBase
{
    private readonly ILogger<OrderNumberController> _logger;
    private readonly IOrderNumberService _orderNumberService;

    public OrderNumberController(
        ILogger<OrderNumberController> logger,
        IOrderNumberService orderNumberService)
    {
        _logger = logger;
        _orderNumberService = orderNumberService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddNumberRequest request)
    {
        var result = await _orderNumberService.Add(request.Number);
        return Ok(new ItemModifiedResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItemRequest<OrderNumber> request)
    {
        var result = await _orderNumberService.Update(request.Item.Id, request.Item.Number);

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
        var result = await _orderNumberService.Delete(request.Id);
        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound(result);
    }
}
