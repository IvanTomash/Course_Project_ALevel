using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogPublisherServiceTest
{
    private readonly ICatalogPublisherService _catalogPublisherService;

    private readonly Mock<ICatalogPublisherRepository> _catalogPublisherRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogPublisher _testPublisher = new CatalogPublisher()
    {
        Publisher = "TestGenre"
    };

    public CatalogPublisherServiceTest()
    {
        _catalogPublisherRepository = new Mock<ICatalogPublisherRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogPublisherService = new CatalogPublisherService(_dbContextWrapper.Object, _logger.Object, _catalogPublisherRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        int? testResult = 1;

        _catalogPublisherRepository.Setup(s => s.Add(
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogPublisherService.Add(_testPublisher.Publisher);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        int? testResult = null;

        _catalogPublisherRepository.Setup(s => s.Add(
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogPublisherService.Add(_testPublisher.Publisher);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Success()
    {
        int? testResult = 1;
        _catalogPublisherRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

        var result = await _catalogPublisherService.Delete(_testPublisher.Id);
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Delete_Failed()
    {
        int? testResult = null;
        _catalogPublisherRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

        var result = await _catalogPublisherService.Delete(_testPublisher.Id);
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Success()
    {
        int? testResult = 1;

        _catalogPublisherRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogPublisherService.Update(_testPublisher.Id, _testPublisher.Publisher);

        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Failed()
    {
        int? testResult = null;

        _catalogPublisherRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        var result = await _catalogPublisherService.Update(_testPublisher.Id, _testPublisher.Publisher);

        result.Should().Be(testResult);
    }
}
