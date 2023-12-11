using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Repositories;

public class OrderGameRepository : IOrderGameRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderGameRepository> _logger;

    public OrderGameRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderGameRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderGame>> GetOrderGames()
    {
        IQueryable<OrderGame> query = _dbContext.OrderGames;
        var items = await query.OrderBy(c => c.Name)
            .Include(i => i.OrderNumber)
            .ToListAsync();
        return items;
    }

    public async Task<int?> Add(int productId, string name, decimal price, int count, int orderNumberId)
    {
        var item = await _dbContext.AddAsync(new OrderGame
        {
            OrderNumberId = orderNumberId,
            Count = count,
            Price = price,
            Name = name,
            ProductId = productId,
        });
        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var item = await _dbContext.OrderGames.FindAsync(id);

        if (item != null)
        {
            var removingItem = _dbContext.OrderGames.Remove(item);
            await _dbContext.SaveChangesAsync();

            return removingItem.Entity.Id;
        }

        return null;
    }

    public async Task<int?> Update(int id, int productId, string name, decimal price, int count, int orderNumberId)
    {
        var item = await _dbContext.OrderGames.FindAsync(id);

        if (item != null)
        {
            item.OrderNumberId = orderNumberId;
            item.Count = count;
            item.Price = price;
            item.Name = name;
            item.ProductId = productId;

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        return null;
    }
}
