using Order.Host.Data.Entities;

namespace Order.Host.Data;

public class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        /*
        if (!context.OrderNumbers.Any())
        {
            await context.OrderNumbers.AddRangeAsync(GetPreconfiguredNumbers());

            await context.SaveChangesAsync();
        }

        if (!context.OrderGames.Any())
        {
            await context.OrderGames.AddRangeAsync(GetPreconfiguredGames());

            await context.SaveChangesAsync();
        }*/
    }

    /*public static IEnumerable<OrderNumber> GetPreconfiguredNumbers()
    {
        return new List<OrderNumber>()
        {
            new OrderNumber() { Number = Guid.NewGuid().ToString() }
        };
    }

    public static IEnumerable<OrderGame> GetPreconfiguredGames()
    {
        return new List<OrderGame>()
        {
            new OrderGame()
            {
                ProductId = 0,
                Name = "Zero",
                Price = 0,
                Count = 0,
                OrderNumberId = 1,
            }
        };
    }*/
    }
