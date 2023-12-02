using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<Catalog> GetCatalogGames(int page, int take, int? genre, int? publisher);
    Task<IEnumerable<SelectListItem>> GetGenres();
    Task<IEnumerable<SelectListItem>> GetPublishers();
}
