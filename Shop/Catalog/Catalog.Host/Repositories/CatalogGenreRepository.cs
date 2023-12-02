using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Catalog.Host.Repositories;

public class CatalogGenreRepository : ICatalogGenreRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogGenreRepository> _logger;

    public CatalogGenreRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogGenreRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogGenre>> GetByPageAsync()
    {
        var totalItems = await _dbContext.CatalogGenres.LongCountAsync();
        var itemsOnPage = await _dbContext.CatalogGenres.OrderBy(c => c.Id).ToListAsync();

        return new PaginatedItems<CatalogGenre>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string genre)
    {
        var item = await _dbContext.AddAsync(new CatalogGenre
        {
            Genre = genre
        });

        return item.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.CatalogGenres.FindAsync(id);

        if (item != null)
        {
            var removingItem = _dbContext.CatalogGenres.Remove(item);
            await _dbContext.SaveChangesAsync();

            return removingItem.Entity.Id;
        }

        return null;
    }

    public async Task<int?> Update(int id, string genre)
    {
        var item = await _dbContext.CatalogGenres.FindAsync(id);

        if (item != null)
        {
            item.Genre = genre;
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        return null;
    }
}
