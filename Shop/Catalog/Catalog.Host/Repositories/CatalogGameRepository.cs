using System.Linq;
using System.Xml.Linq;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogGameRepository : ICatalogGameRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogGameRepository> _logger;

    public CatalogGameRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogGameRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogGame>> GetByPageAsync(int pageIndex, int pageSize, int? genreFilter, int? publisherFilter)
    {
        IQueryable<CatalogGame> query = _dbContext.CatalogGames;

        if (genreFilter.HasValue)
        {
            query = query.Where(w => w.CatalogGenreId == genreFilter.Value);
        }

        if (publisherFilter.HasValue)
        {
            query = query.Where(w => w.CatalogPublisherId == publisherFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
            .Include(i => i.CatalogGenre)
            .Include(i => i.CatalogPublisher)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogGame>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<CatalogGame> GetById(int id)
    {
        var result = await _dbContext.CatalogGames.FindAsync(id);
        return result!;
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogGame
        {
            CatalogGenreId = catalogGenreId,
            CatalogPublisherId = catalogPublisherId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.CatalogGames.FindAsync(id);

        if (item != null)
        {
            var removingItem = _dbContext.CatalogGames.Remove(item);

            await _dbContext.SaveChangesAsync();

            return removingItem.Entity.Id;
        }

        return null;
    }

    public async Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogGenreId, int catalogPublisherId, string pictureFileName)
    {
        var item = await _dbContext.CatalogGames.FindAsync(id);
        if (item != null)
        {
            item.CatalogGenreId = catalogGenreId;
            item.CatalogPublisherId = catalogPublisherId;
            item.Description = description;
            item.Name = name;
            item.PictureFileName = pictureFileName;
            item.Price = price;

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        return null;
    }
}
