using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogGenres.Any())
        {
            await context.CatalogGenres.AddRangeAsync(GetPreconfiguredCatalogGenres());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogPublishers.Any())
        {
            await context.CatalogPublishers.AddRangeAsync(GetPreconfiguredCatalogPublishers());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogGames.Any())
        {
            await context.CatalogGames.AddRangeAsync(GetPreconfiguredGames());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogGenre> GetPreconfiguredCatalogGenres()
    {
        return new List<CatalogGenre>()
        {
            new CatalogGenre() { Genre = "Action/RPG" },
            new CatalogGenre() { Genre = "Adventure" },
            new CatalogGenre() { Genre = "Sports" },
            new CatalogGenre() { Genre = "Shooter" },
            new CatalogGenre() { Genre = "Fighting" }
        };
    }

    private static IEnumerable<CatalogPublisher> GetPreconfiguredCatalogPublishers()
    {
        return new List<CatalogPublisher>()
        {
            new CatalogPublisher() { Publisher = "Sony" },
            new CatalogPublisher() { Publisher = "Electronic Arts" },
            new CatalogPublisher() { Publisher = "Warner Bros. Games" },
            new CatalogPublisher() { Publisher = "Activision" },
            new CatalogPublisher() { Publisher = "Ubisoft" }
        };
    }

    private static IEnumerable<CatalogGame> GetPreconfiguredGames()
    {
        return new List<CatalogGame>()
        {
            new CatalogGame { CatalogGenreId = 1, CatalogPublisherId = 1, AvailableStock = 100, Description = "Horizon Zero Dawn", Name = "Horizon Zero Dawn", Price = 349M, PictureFileName = "1.png" },
            new CatalogGame { CatalogGenreId = 2, CatalogPublisherId = 3, AvailableStock = 100, Description = "Suicide Squad", Name = "Suicide Squad", Price = 2799M, PictureFileName = "2.png" },
            new CatalogGame { CatalogGenreId = 3, CatalogPublisherId = 2, AvailableStock = 100, Description = "EA Sports FC 24", Name = "EA Sports FC 24", Price = 2190M, PictureFileName = "3.png" },
            new CatalogGame { CatalogGenreId = 1, CatalogPublisherId = 3, AvailableStock = 100, Description = "Hogwarts Legacy", Name = "Hogwarts Legacy", Price = 1799M, PictureFileName = "4.png" },
            new CatalogGame { CatalogGenreId = 2, CatalogPublisherId = 1, AvailableStock = 100, Description = "Spider-Man 2", Name = "Spider-Man 2", Price = 2150M, PictureFileName = "5.png" },
            new CatalogGame { CatalogGenreId = 2, CatalogPublisherId = 1, AvailableStock = 100, Description = "The Last Of Us 1", Name = "The Last Of Us 1", Price = 1890M, PictureFileName = "6.png" },
            new CatalogGame { CatalogGenreId = 4, CatalogPublisherId = 4, AvailableStock = 100, Description = "Call of Duty: Modern Warfare III", Name = "Call of Duty: Modern Warfare III", Price = 2350M, PictureFileName = "7.png" },
            new CatalogGame { CatalogGenreId = 1, CatalogPublisherId = 2, AvailableStock = 100, Description = "Star Wars Jedi: Survivor", Name = "Star Wars Jedi: Survivor", Price = 1799M, PictureFileName = "8.png" },
            new CatalogGame { CatalogGenreId = 1, CatalogPublisherId = 5, AvailableStock = 100, Description = "Assassin’s Creed Mirage", Name = "Assassin’s Creed Mirage", Price = 1690M, PictureFileName = "9.png" },
            new CatalogGame { CatalogGenreId = 3, CatalogPublisherId = 2, AvailableStock = 100, Description = "UFC 5", Name = "UFC 5", Price = 2399M, PictureFileName = "10.png" },
            new CatalogGame { CatalogGenreId = 1, CatalogPublisherId = 5, AvailableStock = 100, Description = "Skull & Bones", Name = "Skull & Bones", Price = 1795M, PictureFileName = "11.png" },
            new CatalogGame { CatalogGenreId = 4, CatalogPublisherId = 2, AvailableStock = 100, Description = "Dead Space", Name = "Dead Space", Price = 1750M, PictureFileName = "12.png" },
            new CatalogGame { CatalogGenreId = 5, CatalogPublisherId = 3, AvailableStock = 100, Description = "Mortal Kombat 1", Name = "Mortal Kombat 1", Price = 1890M, PictureFileName = "13.png" },
            new CatalogGame { CatalogGenreId = 1, CatalogPublisherId = 4, AvailableStock = 100, Description = "Diablo IV", Name = "Diablo IV", Price = 2890M, PictureFileName = "14.png" }
        };
    }
}
