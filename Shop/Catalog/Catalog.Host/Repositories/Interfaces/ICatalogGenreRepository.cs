using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogGenreRepository
{
    Task<PaginatedItems<CatalogGenre>> GetByPageAsync();
    Task<int?> Add(string genre);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string genre);
}
