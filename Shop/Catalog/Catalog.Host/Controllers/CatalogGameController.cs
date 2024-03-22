using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Scope("catalog.catalogitem")]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogGameController : ControllerBase
{
    private readonly ILogger<CatalogGameController> _logger;
    private readonly ICatalogGameService _catalogGameService;

    public CatalogGameController(
        ILogger<CatalogGameController> logger,
        ICatalogGameService catalogGameService)
    {
        _logger = logger;
        _catalogGameService = catalogGameService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddGameRequest request)
    {
        var result = await _catalogGameService.Add(
            request.Name,
            request.Description,
            request.Price,
            request.AvailableStock,
            request.CatalogGenreId,
            request.CatalogPublisherId,
            request.PictureFileName);
        return Ok(new ItemModifiedResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItem<CatalogGame> request)
    {
        var result = await _catalogGameService.Update(
            request.Item.Id,
            request.Item.Name,
            request.Item.Description,
            request.Item.Price,
            request.Item.AvailableStock,
            request.Item.CatalogGenreId,
            request.Item.CatalogPublisherId,
            request.Item.PictureFileName);

        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteItemRequest<int> request)
    {
        var result = await _catalogGameService.Delete(request.Id);
        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound(result);
    }
}
