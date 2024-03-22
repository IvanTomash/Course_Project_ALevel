using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogPublisherController : ControllerBase
{
    private readonly ILogger<CatalogPublisherController> _logger;
    private readonly ICatalogPublisherService _catalogPublisherService;

    public CatalogPublisherController(
        ILogger<CatalogPublisherController> logger,
        ICatalogPublisherService catalogPublisherService)
    {
        _logger = logger;
        _catalogPublisherService = catalogPublisherService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddPublisherRequest request)
    {
        var result = await _catalogPublisherService.Add(request.Publisher);
        return Ok(new ItemModifiedResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItem<CatalogPublisher> request)
    {
        var result = await _catalogPublisherService.Update(request.Item.Id, request.Item.Publisher);

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
        var result = await _catalogPublisherService.Delete(request.Id);
        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound(result);
    }
}
