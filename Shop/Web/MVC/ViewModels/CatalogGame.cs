namespace MVC.ViewModels;

public record CatalogGame
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string PictureUrl { get; set; } = null!;

    public CatalogGenre CatalogGenre { get; set; } = null!;

    public CatalogPublisher CatalogPublisher { get; set; } = null!;

    public int AvailableStock { get; set; }
}