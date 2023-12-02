using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Models.Responses;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogGames(int page, int take, int? genre, int? publisher)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (genre.HasValue)
        {
            filters.Add(CatalogTypeFilter.Genre, genre.Value);
        }
        
        if (publisher.HasValue)
        {
            filters.Add(CatalogTypeFilter.Publisher, publisher.Value);
        }
        
        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/GetGames",
           HttpMethod.Post, 
           new PaginatedItemsRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetGenres()
    {
        var genres = await _httpClient.SendAsync<ItemsResponse<CatalogGenre>, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/GetGenres", HttpMethod.Post, null);
        var list = genres.Data.ToList();
        var result = new List<SelectListItem>();
        foreach (var item in list)
        {
            result.Add(new SelectListItem(item.Genre, item.Id.ToString()));
        }

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetPublishers()
    {
        var publishers = await _httpClient.SendAsync<ItemsResponse<CatalogPublisher>, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/GetPublishers", HttpMethod.Post, null);
        var list = publishers.Data.ToList();
        var result = new List<SelectListItem>();
        foreach (var item in list)
        {
            result.Add(new SelectListItem(item.Publisher, item.Id.ToString()));
        }

        return result;
    }
}
