namespace Catalog.Host.Services.Interfaces;

public interface ICatalogGenreService
{
    Task<int?> Add(string genre);
    Task<int?> Delete(int id);
    Task<int?> Update(int id, string genre);
}
