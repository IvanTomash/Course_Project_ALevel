using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Repositories;

public class OrderNumberRepository : IOrderNumberRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderNumberRepository> _logger;

    public OrderNumberRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderNumberRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderNumber>> GetOrderNumbers()
    {
       var items = await _dbContext.OrderNumbers.OrderBy(ci => ci.Id).ToListAsync();
       return items;
    }

    public async Task<int?> Add(string number, string? personId)
    {
        var item = await _dbContext.AddAsync(new OrderNumber
        {
            Number = number,
            PersonId = personId!
        });
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.OrderNumbers.FindAsync(id);

        if (item != null)
        {
            var removingItem = _dbContext.OrderNumbers.Remove(item);
            await _dbContext.SaveChangesAsync();
            return removingItem.Entity.Id;
        }

        _logger.LogInformation($"OrderNumber for deleting isn't found!");
        return null;
    }

    public async Task<int?> Update(int id, string number)
    {
        var item = await _dbContext.OrderNumbers.FindAsync(id);

        if (item != null)
        {
            item.Number = number;
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        _logger.LogInformation($"OrderNumber for updating isn't found!");
        return null;
    }
}
