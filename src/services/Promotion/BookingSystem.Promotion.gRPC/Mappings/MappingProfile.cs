using AutoMapper;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.gRPC.Protos;

namespace BookingSystem.Promotion.gRPC.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PromotionViewModel, PromotionEntity>().ReverseMap();
    }
}
