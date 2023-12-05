using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Basket.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.Get(basketId! + "product");
        _logger.LogInformation($"Response basket: {JsonConvert.SerializeObject(response)}");
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddProduct(AddProductRequest request)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        await _basketService.AddProduct(basketId! + "product", request);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveProduct(RemoveProductRequest request)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        _logger.LogInformation($"Basket id: {basketId}, Product id: {request.Id}");
        await _basketService.RemoveProduct(basketId! + "product", request.Id);
        return Ok();
    }
}