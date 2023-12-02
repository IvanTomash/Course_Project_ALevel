using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogGame, CatalogGameDto>()
            .ForMember("PictureUrl", opt =>
            opt.MapFrom<CatalogGamePictureResolver, string>(c => c.PictureFileName));
        CreateMap<CatalogGenre, CatalogGenreDto>();
        CreateMap<CatalogPublisher, CatalogPublisherDto>();
    }
}
