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
public class CatalogGenreController : ControllerBase
{
    private readonly ILogger<CatalogGenreController> _logger;
    private readonly ICatalogGenreService _catalogGenreService;

    public CatalogGenreController(
        ILogger<CatalogGenreController> logger,
        ICatalogGenreService catalogGenreService)
    {
        _logger = logger;
        _catalogGenreService = catalogGenreService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddGenreRequest request)
    {
        var result = await _catalogGenreService.Add(request.Genre);
        return Ok(new ItemModifiedResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemModifiedResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItem<CatalogGenre> request)
    {
        var result = await _catalogGenreService.Update(request.Item.Id, request.Item.Genre);

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
        var result = await _catalogGenreService.Delete(request.Id);
        if (result != null)
        {
            return Ok(new ItemModifiedResponse<int?>() { Id = result });
        }

        return NotFound(result);
    }
}
