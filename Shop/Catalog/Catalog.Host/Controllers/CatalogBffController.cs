using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogGameDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetGames(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        var result = await _catalogService.GetCatalogGamesAsync(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogGenreDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<CatalogGenreDto>>> GetGenres()
    {
        var result = await _catalogService.GetCatalogGenresAsync();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogGenreDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<CatalogGenreDto>>> GetPublishers()
    {
        var result = await _catalogService.GetCatalogPublishersAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogGameDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CatalogGameDto>> GetGamesById(int id)
    {
        return await _catalogService.GetGamesById(id);
    }
}
