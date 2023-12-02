using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogPublisherRepository : ICatalogPublisherRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogPublisherRepository> _logger;

    public CatalogPublisherRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogPublisherRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogPublisher>> GetByPageAsync()
    {
        var totalItems = await _dbContext.CatalogPublishers.LongCountAsync();
        var itemsOnPage = await _dbContext.CatalogPublishers.OrderBy(c => c.Id).ToListAsync();

        return new PaginatedItems<CatalogPublisher>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string publisher)
    {
        var item = await _dbContext.AddAsync(new CatalogPublisher
        {
            Publisher = publisher
        });

        return item.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.CatalogPublishers.FindAsync(id);

        if (item != null)
        {
            var removingItem = _dbContext.CatalogPublishers.Remove(item);
            await _dbContext.SaveChangesAsync();

            return removingItem.Entity.Id;
        }

        return null;
    }

    public async Task<int?> Update(int id, string publisher)
    {
        var item = await _dbContext.CatalogPublishers.FindAsync(id);

        if (item != null)
        {
            item.Publisher = publisher;
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        return null;
    }
}
