using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogGameRepository _catalogGameRepository;
    private readonly ICatalogGenreRepository _catalogGenreRepository;
    private readonly ICatalogPublisherRepository _catalogPublisherRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogGameRepository catalogGameRepository,
        ICatalogGenreRepository catalogGenreRepository,
        ICatalogPublisherRepository catalogPublisherRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogGameRepository = catalogGameRepository;
        _catalogGenreRepository = catalogGenreRepository;
        _catalogPublisherRepository = catalogPublisherRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogGameDto>?> GetCatalogGamesAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters)
    {
        return await ExecuteSafeAsync(async () =>
        {
            int? genreFilter = null;
            int? publisherFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(CatalogTypeFilter.Genre, out var genre))
                {
                    genreFilter = genre;
                }

                if (filters.TryGetValue(CatalogTypeFilter.Publisher, out var publisher))
                {
                    publisherFilter = publisher;
                }
            }

            var result = await _catalogGameRepository.GetByPageAsync(pageIndex, pageSize, genreFilter, publisherFilter);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogGameDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogGameDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
     }

    public async Task<ItemsResponse<CatalogGenreDto>> GetCatalogGenresAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogGenreRepository.GetByPageAsync();
            return new ItemsResponse<CatalogGenreDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogGenreDto>(s)).ToList()
            };
        });
    }

    public async Task<ItemsResponse<CatalogPublisherDto>> GetCatalogPublishersAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogPublisherRepository.GetByPageAsync();
            return new ItemsResponse<CatalogPublisherDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogPublisherDto>(s)).ToList()
            };
        });
    }

    public async Task<CatalogGameDto> GetGamesById(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogGameRepository.GetById(id);
            return new CatalogGameDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                PictureUrl = result.PictureFileName,
                CatalogGenre = new CatalogGenreDto()
                {
                    Id = result.CatalogGenre.Id,
                    Genre = result.CatalogGenre.Genre
                },
                CatalogPublisher = new CatalogPublisherDto()
                {
                    Id = result.CatalogPublisher.Id,
                    Publisher = result.CatalogPublisher.Publisher,
                },
                AvailableStock = result.AvailableStock
            };
        });
    }
}
