using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogGameDto>?> GetCatalogGamesAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
    Task<ItemsResponse<CatalogGenreDto>> GetCatalogGenresAsync();
    Task<ItemsResponse<CatalogPublisherDto>> GetCatalogPublishersAsync();
    Task<CatalogGameDto> GetGamesById(int id);
}
