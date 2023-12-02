namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogGameService
    {
        Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName);
        Task<int?> Delete(int id);
        Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName);
    }
}
