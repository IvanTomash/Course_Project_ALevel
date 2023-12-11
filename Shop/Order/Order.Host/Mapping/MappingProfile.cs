using AutoMapper;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Microsoft.Extensions.Options;

namespace Order.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderNumber, OrderNumberDto>();
        CreateMap<OrderGame, OrderGameDto>();
    }
}
