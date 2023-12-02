using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.CatalogViewModels;

public class IndexViewModel
{
    public IEnumerable<CatalogGame> CatalogGames { get; set; }
    public IEnumerable<SelectListItem> Genres { get; set; }
    public IEnumerable<SelectListItem> Publishers { get; set; }
    public int? GenreFilterApplied { get; set; }
    public int? PublisherFilterApplied { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
}
