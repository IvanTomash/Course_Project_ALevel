using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogGameRepository> _catalogGameRepository;
    private readonly Mock<ICatalogGenreRepository> _catalogGenreRepository;
    private readonly Mock<ICatalogPublisherRepository> _catalogPublisherRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogGameRepository = new Mock<ICatalogGameRepository>();
        _catalogGenreRepository = new Mock<ICatalogGenreRepository>();
        _catalogPublisherRepository = new Mock<ICatalogPublisherRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(
            _dbContextWrapper.Object,
            _logger.Object,
            _catalogGameRepository.Object,
            _catalogGenreRepository.Object,
            _catalogPublisherRepository.Object,
            _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogGamesAsync_Success()
    {
        int testPageindex = 0;
        int testPageSize = 4;
        int testTotalCount = 4;
        int genreFilter = 2;
        int publisherFilter = 2;
        var filters = new Dictionary<CatalogTypeFilter, int>();
        filters.Add(CatalogTypeFilter.Genre, genreFilter);
        filters.Add(CatalogTypeFilter.Publisher, publisherFilter);

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogGame>()
        {
            Data = new List<CatalogGame>()
            {
                new CatalogGame()
                {
                    Name = "TestName",
                }
            },
            TotalCount = testTotalCount,
        };

        _catalogGameRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageindex),
            It.Is<int>(i => i == testPageSize),
            It.Is<int>(i => i == genreFilter),
            It.Is<int>(i => i == publisherFilter))).ReturnsAsync(pagingPaginatedItemsSuccess);

        var result = await _catalogService.GetCatalogGamesAsync(testPageSize, testPageindex, filters);

        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageSize.Should().Be(testPageSize);
        result?.PageIndex.Should().Be(testPageindex);
    }

    [Fact]
    public async Task GetCatalogGamesAsync_Failed()
    {
        int testPageindex = 0;
        int testPageSize = 4;
        int genreFilter = 2;
        int publisherFilter = 2;
        var filters = new Dictionary<CatalogTypeFilter, int>();
        filters.Add(CatalogTypeFilter.Genre, genreFilter);
        filters.Add(CatalogTypeFilter.Publisher, publisherFilter);

        _catalogGameRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageindex),
            It.Is<int>(i => i == testPageSize),
            It.Is<int>(i => i == genreFilter),
            It.Is<int>(i => i == publisherFilter))).Returns((Func<PaginatedItemsResponse<CatalogGameDto>>)null!);

        var result = await _catalogService.GetCatalogGamesAsync(testPageSize, testPageindex, filters);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogGenresAsync_Success()
    {
        var totalCount = 1;
        var paginatedItems = new PaginatedItems<CatalogGenre>()
        {
            Data = new List<CatalogGenre>()
            {
                new CatalogGenre()
                {
                    Genre = "TestName"
                }
            },

            TotalCount = totalCount
        };

        _catalogGenreRepository.Setup(s => s.GetByPageAsync()).ReturnsAsync(paginatedItems);

        var result = await _catalogService.GetCatalogGenresAsync();
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(totalCount);
    }

    [Fact]
    public async Task GetCatalogGenresAsync_Failed()
    {
        _catalogGenreRepository.Setup(s => s.GetByPageAsync()).Returns((Func<PaginatedItems<CatalogGenreDto>>)null!);

        var result = await _catalogService.GetCatalogGenresAsync();
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogPublishersAsync_Success()
    {
        var totalCount = 1;
        var paginatedItems = new PaginatedItems<CatalogPublisher>()
        {
            Data = new List<CatalogPublisher>()
            {
                new CatalogPublisher()
                {
                    Publisher = "TestName"
                }
            },

            TotalCount = totalCount
        };

        _catalogPublisherRepository.Setup(s => s.GetByPageAsync()).ReturnsAsync(paginatedItems);

        var result = await _catalogService.GetCatalogPublishersAsync();
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(totalCount);
    }

    [Fact]
    public async Task GetCatalogPublishersAsync_Failed()
    {
        _catalogPublisherRepository.Setup(s => s.GetByPageAsync()).Returns((Func<PaginatedItems<CatalogGenreDto>>)null!);

        var result = await _catalogService.GetCatalogPublishersAsync();
        result.Should().BeNull();
    }
}
