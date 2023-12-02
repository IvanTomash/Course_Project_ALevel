#pragma warning disable CS8618
namespace Catalog.Host.Data.Entities;

public class CatalogGame
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string PictureFileName { get; set; }

    public int CatalogGenreId { get; set; }

    public CatalogGenre CatalogGenre { get; set; }

    public int CatalogPublisherId { get; set; }

    public CatalogPublisher CatalogPublisher { get; set; }

    public int AvailableStock { get; set; }
}
