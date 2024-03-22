namespace Catalog.Host.Models.Requests.AddRequests;

public class AddGameRequest
{
    public string Name { get; set; } = null !;

    public string Description { get; set; } = null !;

    public decimal Price { get; set; }

    public int AvailableStock { get; set; }

    public int CatalogGenreId { get; set; }

    public int CatalogPublisherId { get; set; }

    public string PictureFileName { get; set; } = null!;
}
