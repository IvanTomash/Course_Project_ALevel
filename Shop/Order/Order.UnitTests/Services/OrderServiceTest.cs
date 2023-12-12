using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Responses;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTests.Services;

public class OrderServiceTest
{
    private readonly IOrderService _orderService;

    private readonly Mock<IOrderGameRepository> _orderGameRepository;
    private readonly Mock<IOrderNumberRepository> _orderNumberRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrderService>> _logger;

    public OrderServiceTest()
    {
        _orderGameRepository = new Mock<IOrderGameRepository>();
        _orderNumberRepository = new Mock<IOrderNumberRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrderService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _orderService = new OrderService(
            _dbContextWrapper.Object,
            _logger.Object,
            _orderGameRepository.Object,
            _orderNumberRepository.Object,
            _mapper.Object);
    }

    [Fact]
    public async Task GetOrderGamesAsync_Success()
    {
        var testGames = new List<OrderGame>
        {
            new OrderGame
            {
                Id = 1,
                ProductId = 1,
                Name = "Test",
                Price = 1,
                Count = 1,
                OrderNumberId = 1,
                OrderNumber = new OrderNumber
                {
                    Id = 1,
                    PersonId = "testid",
                    Number = "test"
                }
            }
        };
        _orderGameRepository.Setup(s => s.GetOrderGames()).ReturnsAsync(testGames);

        var result = await _orderService.GetOrderGamesAsync();

        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(1);
    }

    [Fact]
    public async Task GetOrderGamesAsync_Failed()
    {
        _orderGameRepository.Setup(s => s.GetOrderGames()).ReturnsAsync((Func<IEnumerable<OrderGame>>)null!);

        var result = await _orderService.GetOrderGamesAsync();

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetOrderNumbersAsync_Success()
    {
        var testNumbers = new List<OrderNumber>
        {
            new OrderNumber
            {
                Id = 1,
                Number = "test",
                PersonId = "testid"
            }
        };
        _orderNumberRepository.Setup(s => s.GetOrderNumbers()).ReturnsAsync(testNumbers);

        var result = await _orderService.GetOrderNumbersAsync();

        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(1);
    }

    [Fact]
    public async Task GetOrderNumbersAsync_Failed()
    {
        _orderNumberRepository.Setup(s => s.GetOrderNumbers()).ReturnsAsync((Func<IEnumerable<OrderNumber>>)null!);

        var result = await _orderService.GetOrderNumbersAsync();

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateOrderAsync_Success()
    {
        int? testRes = 1;
        string? personId = "testid";
        string number = "testNumber";
        int orderNumberId = 1;
        var orderRequest = new CreateOrderRequest
        {
            Data = new List<RequestedProduct>
            {
                new RequestedProduct
                {
                    Id = 1,
                    Name = "test",
                    Price = 10,
                    Count = 1
                }
            }
        };

        _orderNumberRepository.Setup(s => s.Add(
            It.Is<string>(i => i == number),
            It.Is<string>(i => i == personId))).ReturnsAsync(testRes);

        _orderGameRepository.Setup(s => s.Add(
            It.Is<int>(i => i == orderRequest.Data.FirstOrDefault() !.Id),
            It.Is<string>(i => i == orderRequest.Data.FirstOrDefault() !.Name),
            It.Is<decimal>(i => i == orderRequest.Data.FirstOrDefault() !.Price),
            It.Is<int>(i => i == orderRequest.Data.FirstOrDefault() !.Count),
            It.Is<int>(i => i == orderNumberId))).ReturnsAsync(testRes);

        var result = await _orderService.CreateOrderAsync(orderRequest, personId);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateOrderAsync_Failed()
    {
        string? personId = "testid";
        string number = "testNumber";
        int orderNumberId = 1;
        var orderRequest = new CreateOrderRequest
        {
            Data = new List<RequestedProduct>
            {
                new RequestedProduct
                {
                    Id = 1,
                    Name = "test",
                    Price = 10,
                    Count = 1
                }
            }
        };

        _orderNumberRepository.Setup(s => s.Add(
            It.Is<string>(i => i == number),
            It.Is<string>(i => i == personId))).ReturnsAsync((Func<int?>)null!);

        _orderGameRepository.Setup(s => s.Add(
            It.Is<int>(i => i == orderRequest.Data.FirstOrDefault() !.Id),
            It.Is<string>(i => i == orderRequest.Data.FirstOrDefault() !.Name),
            It.Is<decimal>(i => i == orderRequest.Data.FirstOrDefault() !.Price),
            It.Is<int>(i => i == orderRequest.Data.FirstOrDefault() !.Count),
            It.Is<int>(i => i == orderNumberId))).ReturnsAsync((Func<int?>)null!);

        var result = await _orderService.CreateOrderAsync(orderRequest, personId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetOrderAsync_Success()
    {
        var result = await _orderService.GetOrdersAsync();
        result.Should().NotBeNull();
        result?.OrderNumbers.Should().NotBeNull();
        result?.OrderGames.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderAsync_Failed()
    {
        var result = await _orderService.GetOrdersAsync();
        result.Should().NotBeNull();
        result?.OrderNumbers.Should().NotBeNull();
        result?.OrderGames.Should().NotBeNull();
    }
}
