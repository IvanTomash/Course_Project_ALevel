using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogPublisherRepository
{
    Task<PaginatedItems<CatalogPublisher>> GetByPageAsync();
    Task<int?> Add(string publisher);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string publisher);
}
