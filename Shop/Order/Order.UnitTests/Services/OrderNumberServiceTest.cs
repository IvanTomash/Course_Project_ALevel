using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTests.Services;

public class OrderNumberServiceTest
{
    private readonly IOrderNumberService _orderNumberService;

    private readonly Mock<IOrderNumberRepository> _orderNumberRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrderService>> _logger;

    private readonly OrderNumber _testNumber = new OrderNumber()
    {
        Id = 1,
        Number = "test",
        PersonId = "id"
    };

    public OrderNumberServiceTest()
    {
        _orderNumberRepository = new Mock<IOrderNumberRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrderService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _orderNumberService = new OrderNumberService(
            _dbContextWrapper.Object,
            _logger.Object,
            _orderNumberRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        int? testRes = 1;

        _orderNumberRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string?>())).ReturnsAsync(testRes);

        var result = await _orderNumberService.Add(_testNumber.Number, _testNumber.PersonId);

        result.Should().Be(testRes);
    }

    [Fact]
    public async Task Add_Failed()
    {
        _orderNumberRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string?>())).ReturnsAsync((Func<int?>)null!);

        var result = await _orderNumberService.Add(_testNumber.Number, _testNumber.PersonId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Delete_Success()
    {
        int? testRes = 1;

        _orderNumberRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync(testRes);

        var result = await _orderNumberService.Delete(_testNumber.Id);

        result.Should().Be(testRes);
    }

    [Fact]
    public async Task Delete_Failed()
    {
        _orderNumberRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync((Func<int?>)null!);

        var result = await _orderNumberService.Delete(_testNumber.Id);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Update_Success()
    {
        int? testRes = 1;

        _orderNumberRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testRes);

        var result = await _orderNumberService.Update(_testNumber.Id, _testNumber.Number);

        result.Should().Be(testRes);
    }

    [Fact]
    public async Task Update_Failed()
    {
        _orderNumberRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync((Func<int?>)null!);

        var result = await _orderNumberService.Update(_testNumber.Id, _testNumber.PersonId);

        result.Should().BeNull();
    }
}
