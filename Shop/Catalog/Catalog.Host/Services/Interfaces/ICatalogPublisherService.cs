namespace Catalog.Host.Services.Interfaces;

public interface ICatalogPublisherService
{
    Task<int?> Add(string publisher);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string publisher);
}
