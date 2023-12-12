using AutoMapper;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Responses;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
{
    private readonly IOrderGameRepository _orderGameRepository;
    private readonly IOrderNumberRepository _orderNumberRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderGameRepository orderGameRepository,
        IOrderNumberRepository orderNumberRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _orderGameRepository = orderGameRepository;
        _orderNumberRepository = orderNumberRepository;
        _mapper = mapper;
    }

    public async Task<ItemsResponse<OrderGameDto>> GetOrderGamesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderGameRepository.GetOrderGames();
            return new ItemsResponse<OrderGameDto>()
            {
                Count = result.Count(),
                Data = result.Select(s => _mapper.Map<OrderGameDto>(s)).ToList()
            };
        });
    }

    public async Task<ItemsResponse<OrderNumberDto>> GetOrderNumbersAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderNumberRepository.GetOrderNumbers();
            return new ItemsResponse<OrderNumberDto>()
            {
                Count = result.Count(),
                Data = result.Select(s => _mapper.Map<OrderNumberDto>(s)).ToList()
            };
        });
    }

    public async Task<int?> CreateOrderAsync(CreateOrderRequest request, string? personId)
    {
        var orderId = await _orderNumberRepository.Add(Guid.NewGuid().ToString(), personId);
        foreach (var item in request.Data)
        {
            await _orderGameRepository.Add(item.Id, item.Name, item.Price, item.Count, orderId ?? default(int));
        }

        return orderId;
    }

    public async Task<OrderResponse> GetOrdersAsync()
    {
        var orderNumbers = await GetOrderNumbersAsync();
        var orderGames = await GetOrderGamesAsync();

        var result = new OrderResponse()
        {
            OrderNumbers = orderNumbers.Data,
            OrderGames = orderGames.Data,
        };
        return result;
    }
}
