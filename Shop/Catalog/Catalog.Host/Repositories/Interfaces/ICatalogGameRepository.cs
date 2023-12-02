using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogGameRepository
{
    Task<PaginatedItems<CatalogGame>> GetByPageAsync(int pageIndex, int pageSize, int? genreFilter, int? publisherFilter);
    Task<CatalogGame> GetById(int id);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName);
}
