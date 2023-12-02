namespace MVC.ViewModels;

public record Catalog
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int Count { get; init; }
    public List<CatalogGame> Data { get; init; }
}
